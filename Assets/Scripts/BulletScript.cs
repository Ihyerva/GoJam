using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyBulletType {
    melee,
    ranged
 
}


public class BulletScript : MonoBehaviour
{

    [SerializeField]private GameObject _gameManager;
    [SerializeField]
    private int _damage;
    [SerializeField] private enemyBulletType type;
    private Transform target;
    [SerializeField]
    private float speed;
    private Rigidbody2D rb2D;


    private void Awake()
    {
        _gameManager = GameObject.Find("Game Manager");
    }
    private void Start()
    {
                if(type==enemyBulletType.melee)
                StartCoroutine("enemySpawnDamage");


        
    }

    private void Update()
    {

        if(type == enemyBulletType.ranged)
        Move();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Base") && type == enemyBulletType.ranged)
        {
            _gameManager.GetComponent<GameManager>().ChangeBaseHealth(_damage);
            Destroy(gameObject);
            
        }
    }

    public void setTarget(Transform newTarget)
    {
        target = newTarget;
        print(target.position);
    }


    public void Move()
    {
        //transform.Translate(target.position * Time.deltaTime * speed);
        transform.position = Vector3.Lerp(transform.position, target.position, .5f * Time.deltaTime);
    }

    private IEnumerator enemySpawnDamage()
    {
        
        transform.position = target.position;
        yield return new WaitForSeconds(0.8f);
        _gameManager.GetComponent<GameManager>().ChangeBaseHealth(_damage);
        Destroy(gameObject,0.1f);
    }
}


