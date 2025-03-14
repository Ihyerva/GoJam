using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    private float timer;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    [SerializeField]
    private float cooldown,bulletSpeed;
    [SerializeField]
    private int price,damage;
    [SerializeField]
    private GameObject bullet, bulletPoint;


    private void Start()
    {
        timer = cooldown;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }

    private void Update()
    {
        if (timer >= cooldown && enemiesInRange.Count > 0)
        {
            Attack();
            timer = 0;
        }
        else if (timer < cooldown)
            timer += Time.deltaTime;

    }

    private void Attack()
    {
        GameObject currentBullet = Instantiate(bullet, bulletPoint.transform.position, bulletPoint.transform.rotation);
        if(enemiesInRange.Count > 0)
            currentBullet.GetComponent<Rigidbody2D>().AddForce((enemiesInRange[0].transform.position - bulletPoint.transform.position).normalized*bulletSpeed);


    }
}
