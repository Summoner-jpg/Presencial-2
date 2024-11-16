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
            Debug.LogWarning("There's no Rigidbody2D attached to the player.");
        }
        _playerRoll = GetComponent<PlayerRoll>();
    }

    void Update()
    {
        _moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;

        _isGrounded = CheckGround();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_playerRoll.IsRolling)
        {
            Vector2 rollDirection = _moveDirection;
            if (rollDirection != Vector2.zero)
            {
                _playerRoll.StartRoll(rollDirection);
            }
        }

        if (_isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (Mathf.Abs(_moveDirection.x) > 0)
        {
            _animator.SetBool("IsMoving", true);

            // Gira al personaje hacia la dirección del movimiento
            if (_moveDirection.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Mirar a la derecha
            }
            else if (_moveDirection.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Mirar a la izquierda
            }
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
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, groundLayer);
        
        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckRadius, Color.red);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
