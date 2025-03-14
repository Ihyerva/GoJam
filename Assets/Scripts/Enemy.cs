using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    

    [SerializeField]
    private int _maxHealth;
    private int _currentHealth;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _moneyGain;
    [SerializeField]
    private float _cooldown;
    private float _timer;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private Transform _baseTransform;
    [SerializeField]
    private Transform _bulletPoint;
    [SerializeField]
    private float _range;
    


    public void Awake()
    {
        _currentHealth = _maxHealth;
        _timer = 0;
         
    }


    public void Update()
    {
        Attack();
        EnemyDespawn();
    }



    public void TakeDamage(int damage)
    {
        _currentHealth-=damage;
    }

  
   public void Attack()
    {
        if (Vector2.Distance(transform.position, _baseTransform.position) <= _range && _timer >= _cooldown)
        {
            GameObject enemyBullet = Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
            enemyBullet.GetComponent<Rigidbody2D>().AddForce((_baseTransform.position- _bulletPoint.position).normalized * _bulletSpeed);

            _timer = 0;

            
        }
        else if (_timer < _cooldown)
        {
            _timer += Time.deltaTime;
        }


    }

    public void EnemyDespawn()
    {
        if (_currentHealth <= 0)
        {

           Destroy(gameObject);
        }
    }


}
