using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float speed;
    private Rigidbody2D rb2D;

    private void Awake()
    {
    }

    private void Update()
    {
        Move();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Base"))
        {
            Destroy(gameObject); 
        }
    }

    public void setTarget(Transform newTarget)
    {
        target = newTarget;
    }


    public void Move()
    {
        transform.Translate(target.position * Time.deltaTime * speed);
    }
}


