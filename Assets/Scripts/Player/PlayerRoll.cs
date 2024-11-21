using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    public float rollDistance = 5f;
    public float rollDuration = 0.5f;
    public LayerMask collisionLayer;

    public Collider2D normalCollider;
    public Collider2D rollCollider;

    private bool _isRolling = false;
    public bool IsRolling => _isRolling;

    private Rigidbody2D _rb;
    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        if (rollCollider != null)
            rollCollider.enabled = false;
    }

    public void StartRoll(Vector2 direction)
    {
        if (!_isRolling)
        {
            StartCoroutine(Roll(direction));
        }
    }

    private IEnumerator Roll(Vector2 direction)
    {
        _isRolling = true;
        _animator.SetBool("IsRolling", true);
        
        if (normalCollider != null) normalCollider.enabled = false;
        if (rollCollider != null) rollCollider.enabled = true;

        direction = direction.normalized;
        Vector2 startPosition = _rb.position;
        Vector2 targetPosition = startPosition + direction * rollDistance;

        float timeElapsed = 0f;
        Vector2 currentPosition;

        while (timeElapsed < rollDuration)
        {

            float lerpFactor = timeElapsed / rollDuration;
            currentPosition = Vector2.Lerp(startPosition, targetPosition, lerpFactor);
            
            RaycastHit2D hit = Physics2D.Raycast(_rb.position, direction, rollDistance * lerpFactor, collisionLayer);
            if (hit.collider != null)
            {
                targetPosition = hit.point - direction * 0.1f;
                break;
            }


            _rb.velocity = (currentPosition - _rb.position) / Time.fixedDeltaTime;
            timeElapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        _rb.velocity = Vector2.zero;
        _rb.position = targetPosition;
        
        if (normalCollider != null) normalCollider.enabled = true;
        if (rollCollider != null) rollCollider.enabled = false;

        _isRolling = false;
        _animator.SetBool("IsRolling", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * rollDistance);
    }
}
