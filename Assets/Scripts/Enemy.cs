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
    private GameObject _bulletPrefab,DimensionalCopy;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private Transform _baseTransform;
    [SerializeField]
    private Transform _bulletPoint;
    [SerializeField]
    private float _range;
    [SerializeField]
    private List<Transform> _enemyMoveList = new List<Transform>();
    private bool canClick = false;
    private int _currentTargetIndex = 0;
    


    public void Awake()  
    {
        _currentHealth = _maxHealth;
        _timer = _cooldown;
         
    }


    public void Update()
    {
        if (Vector2.Distance(transform.position, _baseTransform.position) <= _range)
        {
            if (_timer >= _cooldown)
            {
                Attack();
            }
            else if (_timer < _cooldown)
            {
                _timer += Time.deltaTime;
            }
        }
        else
        {
            EnemyMove();
        }
    }



    public void TakeDamage(int damage)
    {
        _currentHealth-=damage;
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

  
   public void Attack()
    {
        GameObject enemyBullet = Instantiate(_bulletPrefab, _bulletPoint.position, _bulletPoint.rotation);
        enemyBullet.GetComponent<Rigidbody2D>().AddForce((_baseTransform.position - _bulletPoint.position).normalized * _bulletSpeed);
        _timer = 0;
    }

   


    public void EnemyMove()
    {
      Transform Target = _enemyMoveList[_currentTargetIndex];
        transform.position=Vector2.MoveTowards(transform.position, Target.position, _speed*Time.deltaTime);
        if (Vector2.Distance(transform.position, Target.position) < 0.1f)
        {
            _currentTargetIndex++;

        }
    }


    private void OnMouseEnter()
    {
        canClick = true;
    }
    private void OnMouseExit()
    {
        canClick = false;
    }

    private void OnMouseDown()
    {
        if (canClick)
        {
            //spawnDimensionalCopy
            Destroy(gameObject);
        }
    }
}
