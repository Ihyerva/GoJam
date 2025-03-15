using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    private float timer;
    [SerializeField]
    private float cooldown,bulletSpeed,range;
    [SerializeField]
    private int price;
    [SerializeField]
    private GameObject bullet, bulletPoint;
    private Transform currentTarget;

    private void Start()
    {
        timer = cooldown;
    }
    

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        bool includes = false;
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].gameObject.transform == currentTarget)
            {
                includes = true;
            }
        }
        if(currentTarget == null || !includes)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].gameObject.tag == "Enemy")
                {
                    currentTarget = hits[i].gameObject.transform;
                    break;
                }
            }
        } 





        if (timer >= cooldown && hits.Length > 0)
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
        currentBullet.GetComponent<TowerBullets>().setTarget(currentTarget);


    }


    public int GetPrice()
    {
        return price;

    }



}
