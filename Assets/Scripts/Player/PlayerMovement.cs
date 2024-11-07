using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private bool _isGrounded;
    private Rigidbody2D _rb;
    private Vector2 _moveDirection;

    private PlayerRoll _playerRoll;

    void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_playerRoll.IsRolling)
        {
            _playerRoll.StartRoll();
        }

        if (_isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (_moveDirection.magnitude > 0 && !_playerRoll.IsRolling)
        {
            _rb.velocity = new Vector2(_moveDirection.x * moveSpeed, _rb.velocity.y);
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}    
    



