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
    private float range,speed;
    [SerializeField]
    private bulletType type;
    private Transform target;
    
   

    private void Start()
    {
        Destroy(gameObject, 5);
        switch(type)
        {
            case bulletType.spawn:
                StartCoroutine("spawnerDamage");
                break;
            case bulletType.thrown:
                gameObject.GetComponent<Rigidbody2D>().AddForce((target.position-transform.position).normalized*speed);
                
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(type == bulletType.thrown && collision.gameObject.tag == "Enemy")
        {
            if(range > 0)
            {
                RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, transform.position);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.tag == "Enemy")
                    {
                        hits[i].collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
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

    private IEnumerator spawnerDamage()
    {
        transform.position = target.position;
        yield return new WaitForSeconds(0.8f);
        target.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject,0.1f);
    }
}
