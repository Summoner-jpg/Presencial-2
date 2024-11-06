using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    private bool _isGrounded = true;
    private bool _facingRight = true;



    void Update()
    {
        float currentX = transform.position.x;
        float currentY = transform.position.y;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            currentX += moveSpeed * Time.deltaTime;
            if (!_facingRight)
            {
                flip();
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            currentX -= moveSpeed * Time.deltaTime;
            if (_facingRight)
            {
                flip();
            }
        }

        if (_isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            currentY += jumpHeight;
            _isGrounded = false;
        }

        transform.position = new Vector3(currentX, currentY, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void flip()
    {
        _facingRight = !_facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}