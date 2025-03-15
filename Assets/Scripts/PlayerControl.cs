using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private List<GameObject> _towers = new List<GameObject>();
    [SerializeField] private int _playerMoney;
    [SerializeField] private float _playerPlacementRange = 20f;
    [SerializeField] private Transform spawnPoint1,spawnPoint2;
    int currentDimension = 1;
    int dimensionShifts = 5;
    GameObject dimensionShiftTarget;
    [SerializeField] private GameEvent _moneyChanged;
    

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }



    public void Update()
    {
        HandleControls();
        TowerPlacement();
       
    }
    public void FixedUpdate()
    {
        HandleMovement();
        
    }

    public void HandleControls()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _movement = _movement.normalized;
    }
    public void HandleMovement()
    {
       

        _rigidbody.velocity = _movement * _playerSpeed;


    }


    public void TowerPlacement()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerMoney >= _towers[0].GetComponent<Tower>().GetPrice())
        {
            bool _canPlace = true;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _playerPlacementRange, transform.position);
            for (int i = 0; i<hits.Length;i++)
            {
                if (hits[i].collider.CompareTag("Tower"))
                {
                    _canPlace = false;
                }

            }
            if (_canPlace) {
                Instantiate(_towers[0], _rigidbody.transform.position, _rigidbody.transform.rotation);
                

                _moneyChanged.Raise(this, -_towers[0].GetComponent<Tower>().GetPrice());


            }


        }
    }


   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playerPlacementRange);
    }

    public void IncreaseMoney(Component sender, object GainMoney)
    {
        _playerMoney += (int) GainMoney;


    }

    public int GetMoney()
    {
        return _playerMoney;
    }

    private void changeDimensionShiftTarget(Component sender, object newTarget)
    {
        dimensionShiftTarget = (GameObject) newTarget;
    }
    
    private void dimensionShift()
    {
        if(currentDimension == 1)
        {
            gameObject.transform.position = spawnPoint2.position;
            currentDimension = 2;
        }
        else if(currentDimension == 1)
        {
            gameObject.transform.position = spawnPoint1.position;
            currentDimension = 2;
        }
    }
    private void OnMouseDown()
    {
        if(Input.GetKey(KeyCode.Space) && dimensionShifts > 0)
        {
            if (dimensionShiftTarget.tag == "Enemy" && dimensionShiftTarget.GetComponent<Enemy>().getDimension() == currentDimension)
            {
                dimensionShiftTarget.GetComponent<Enemy>().dimensionShift();
                dimensionShifts--;
            }
            else if (dimensionShiftTarget.tag == "Player")
            {
                dimensionShift();
                dimensionShifts--;
            }
        }
    }
}
