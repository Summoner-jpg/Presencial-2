using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    public float rollDistance = 5f;
    public float rollDuration = 0.5f;
    private bool _isRolling = false;
    public bool IsRolling => _isRolling;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        
        direction = direction.normalized;
        
        Vector2 startPosition = _rb.position;
        Vector2 targetPosition = startPosition + direction * rollDistance;

        float timeElapsed = 0f;

        while (timeElapsed < rollDuration)
        {
            _rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, timeElapsed / rollDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        _rb.MovePosition(targetPosition);

        _isRolling = false;
    }
}