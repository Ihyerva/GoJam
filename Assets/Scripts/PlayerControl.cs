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
                _playerMoney -= _towers[0].GetComponent<Tower>().GetPrice();
            }


        }
    }


   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _playerPlacementRange);
    }

}
