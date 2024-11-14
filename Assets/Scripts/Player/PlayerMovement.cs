using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    private bool _isGrounded;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private Animator _animator;
    private PlayerRoll _playerRoll;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.LogWarning("There's no rigidbody");
        }
        _playerRoll = GetComponent<PlayerRoll>();
    }

    void Update()
    {
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_playerRoll.IsRolling)
        {
            _playerRoll.StartRoll();
        }

        if (_isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        
        if (Mathf.Abs(_moveDirection.x) > 0)
        {
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }
    }
    
    void FixedUpdate()
    {
        if (_moveDirection.magnitude > 0 && !_playerRoll.IsRolling)
        {
            _rb.velocity = new Vector2(_moveDirection.x * moveSpeed, _rb.velocity.y);
        }
        else if (!_playerRoll.IsRolling)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
