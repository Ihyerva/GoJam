using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float _playerSpeed;
    

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }



    public void Update()
    {
        HandleControls();
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


}
