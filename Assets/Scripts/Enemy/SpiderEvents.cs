using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEvents : MonoBehaviour {

    private Spider _spider;

    private void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        Debug.Log("Spider is firing");
        _spider.Attack();
    }
    
}
