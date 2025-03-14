using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bulletType
{
    spawn,
    thrown,
    area
}
public class TowerBullets : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float range;
    [SerializeField]
    private bulletType type;

    private Transform target;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, transform.position);
            for (int i = 0;i<hits.Length;i++)
            {
                if (hits[i].collider.gameObject.tag == "Enemy")
                {
                    hits[i].collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            Destroy(gameObject);

        }


    }

    public void setTarget(Transform x)
    {
        target = x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}
