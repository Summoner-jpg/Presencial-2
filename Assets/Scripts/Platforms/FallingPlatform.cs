using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1.0f;
    public float respawnDelay = 3.0f;
    
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool isFalling = false;
    private Collider2D platformCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        rb.isKinematic = true;
        platformCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isFalling)
        {
            Invoke("StartFalling", fallDelay);
        }
    }

    void StartFalling()
    {
        isFalling = true;
        rb.isKinematic = false;
        
        platformCollider.isTrigger = false;
        
        Invoke("Disappear", 0.5f);
    }

    void Disappear()
    {
        gameObject.SetActive(false);
        Invoke("Respawn", respawnDelay);
    }

    void Respawn()
    {
        transform.position = initialPosition;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        platformCollider.isTrigger = true;
        gameObject.SetActive(true);
        isFalling = false;
    }
}
