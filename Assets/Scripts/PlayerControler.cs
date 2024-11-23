using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;
    private Animator _playerAnimator;
    public float _playerSpeed;
    private Vector2 _playerDirection;

    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _playerAnimator.SetFloat("Horizontal", _playerDirection.x);
        _playerAnimator.SetFloat("Vertical", _playerDirection.y);
        _playerAnimator.SetFloat("Speed", _playerDirection.magnitude);
    }

    void FixedUpdate()
    {
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerDirection * _playerSpeed * Time.fixedDeltaTime);
    }
}
