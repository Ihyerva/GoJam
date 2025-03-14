using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBottle : MonoBehaviour
{
    [SerializeField]
    private int damage;
    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 3, transform.position);
        foreach (RaycastHit2D hit in hits) {
            if(hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        } 

    }
}
