﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    public GameObject diamondPrefab;

    [SerializeField]  protected int health;
    [SerializeField]  protected int speed;
    [SerializeField]  protected int gems;

    [SerializeField] protected Transform pointA, pointB;

    protected Vector3 currentPoint;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit = false;
    protected bool isDead = false;

    protected Player player;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("inCombat") == false)
        {
            return;
        }

        if (isDead == false)
        {
            Movement();
        }
       
    }

    public virtual void Movement()
    {
        FlipMossGiant();

        //------------------

        if (transform.position == pointA.position)
        {
            currentPoint = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentPoint = pointA.position;
            anim.SetTrigger("Idle");
        }

        if (isHit == false) {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint, speed * Time.deltaTime);
        }

        //------------------

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        if (distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("inCombat", false);
        }

        //------------------

        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && anim.GetBool("inCombat") == true)
        {
            sprite.flipX = false;
        }
        else if (direction.x < 0 && anim.GetBool("inCombat") == true)
        {
            sprite.flipX = true;
        }
    }

    private void FlipMossGiant()
    {
        if (currentPoint == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
