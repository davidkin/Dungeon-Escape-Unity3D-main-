using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

    public int diamonds; 

    private Rigidbody2D _rigid;

    private bool resetJump = false;
    private bool _grounded = false;

    [SerializeField] private float _speedForce = 2.5f;
    [SerializeField] private float _jumpForce = 6f;
    [SerializeField] private LayerMask _groundLayer;

    private PlayerAnimation _playerAnim;

    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int Health { get; set; }

    void Start () {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        Movement();
        PlayerAttack();
    }

    void Movement()
    {
        // Horizontal input for left/right move
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = isGrounded();

        if (move > 0)
        {
            PlayerFlip(true);
        }
        else if (move < 0)
        {
            PlayerFlip(false);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _speedForce, _rigid.velocity.y);

        _playerAnim.Move(move);
    }

    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, _groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.green);

        if (hitInfo.collider != null)
        {
            if (resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    void PlayerFlip(bool moveSide)
    {
        if (moveSide == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (moveSide == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    void PlayerAttack()
    {
        if(Input.GetMouseButtonDown(0) && isGrounded() == true)
        {
            _playerAnim.Attack();
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }

    public void Damage ()
    {
        Debug.Log("Player::Damage()");
    }

}
