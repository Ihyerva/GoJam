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
    [SerializeField]
    GameObject dimensionShiftTarget;
    [SerializeField] private GameEvent _moneyChanged;
    private Animator anim;
    

    public void Awake()
    {
        anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }



    public void Update()
    {
        HandleControls();
        TowerPlacement();


        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.Mouse0) && dimensionShifts > 0)
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
    public void FixedUpdate()
    {
        HandleMovement();
        
    }

    public void HandleControls()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _movement = _movement.normalized;
        if (_movement.x > 0) // Moving Right
        {
            anim.SetInteger("Direction", 0);
            anim.SetBool("IsMoving", true);


        }
        else if (_movement.x < 0) // Moving Left
        {
            anim.SetInteger("Direction", 1);
            anim.SetBool("IsMoving", true);


        }
        else if (_movement.y > 0) // Moving Up
        {
            anim.SetInteger("Direction", 2);
            anim.SetBool("IsMoving", true);


        }
        else if (_movement.y < 0) // Moving Down
        {
            anim.SetInteger("Direction", 3);
            anim.SetBool("IsMoving", true);


        }
        else // Idle (no movement)
        {
            anim.SetBool("IsMoving", false);

        }
    }
    public void HandleMovement()
    {
       

        _rigidbody.velocity = _movement * _playerSpeed;


    }


    public void TowerPlacement()
    {
        
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T)))
        {
            bool _canPlace = true;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _playerPlacementRange, transform.position);

            // Check if the placement area is clear
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.CompareTag("Tower"))
                {
                    _canPlace = false;
                    break; 
                }
            }

            if (_canPlace)
            {
                GameObject towerToPlace = null;

               
                if (Input.GetKeyDown(KeyCode.E) && _playerMoney >= _towers[0].GetComponent<Tower>().GetPrice())
                {
                    towerToPlace = _towers[0];
                }
                else if (Input.GetKeyDown(KeyCode.R) && _playerMoney >= _towers[1].GetComponent<Tower>().GetPrice())
                {
                    towerToPlace = _towers[1];
                }
                else if (Input.GetKeyDown(KeyCode.T) && _playerMoney >= _towers[2].GetComponent<Tower>().GetPrice())
                {
                    towerToPlace = _towers[2];
                }

                
                if (towerToPlace != null)
                {
                    _moneyChanged.Raise(this, -towerToPlace.GetComponent<Tower>().GetPrice());
                    GameObject placedTower = Instantiate(towerToPlace, _rigidbody.transform.position, _rigidbody.transform.rotation);
                    placedTower.GetComponent<Tower>().dimension = currentDimension;
                }
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
        _playerMoney += (int)GainMoney;


    }

    public int GetMoney()
    {
        return _playerMoney;
    }

    public void ChangeDimensionShiftTarget(Component sender, object newTarget)
    {
        dimensionShiftTarget = (GameObject)newTarget;
    }

    private void dimensionShift()
    {
        if (currentDimension == 1)
        {
            gameObject.transform.position = spawnPoint2.position;
            currentDimension = 2;
        }
        else if (currentDimension == 2)
        {
            gameObject.transform.position = spawnPoint1.position;
            currentDimension = 1;
        }
    }

    private void OnMouseEnter()
    {
        dimensionShiftTarget = gameObject;
    }
    private void OnMouseExit()
    {
        dimensionShiftTarget = null;
    }

}


   

  