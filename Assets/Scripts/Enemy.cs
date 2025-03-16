using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int dimension;
    [SerializeField]
    private int _maxHealth;
    private int _currentHealth;
    [SerializeField]
    private float _speed,_cooldown,_bulletSpeed;
    [SerializeField]
    private int _moneyGain;
    private float _timer;
    [SerializeField]
    private GameObject _bulletPrefab,DimensionalCopy;
    public Transform _baseTransform,shiftSpawnLocation;
    [SerializeField]
    private Transform _bulletPoint;
    [SerializeField]
    private float _range;
    [SerializeField]
    private List<Transform> _enemyMoveList = new List<Transform>();
    [SerializeField] private GameEvent _moneyGainEvent,dimensionShiftTargetChangedEvent;
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

            _moneyGainEvent.Raise(this,_moneyGain);
            Destroy(gameObject);
        }
    }

   public void Attack()
    {
        if (_timer >= _cooldown)
        {
            GameObject enemyBullet = Instantiate(_bulletPrefab, _bulletPoint.position, transform.rotation);
            BulletScript bulletScript = enemyBullet.GetComponent<BulletScript>();

            bulletScript.setTarget(_baseTransform);

            _timer = 0; 


        }
        
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
        dimensionShiftTargetChangedEvent.Raise(this,gameObject);
    }
    private void OnMouseExit()
    {
        dimensionShiftTargetChangedEvent.Raise(this, null);
    }

    public void dimensionShift()
    {
        Instantiate(DimensionalCopy, shiftSpawnLocation.position, shiftSpawnLocation.rotation);
        Destroy(gameObject);
    }

    public int getDimension()
    {
        return dimension;
    }
}
