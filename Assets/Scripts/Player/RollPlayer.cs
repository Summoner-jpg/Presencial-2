using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    public float rollDistance = 5f;
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    private bool _isRolling = false;
    public bool IsRolling => _isRolling;
    private Rigidbody2D _rb; 

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void StartRoll()
    {
        if (!_isRolling)
        {
            StartCoroutine(Roll());
        }
    }

    private IEnumerator Roll()
    {
        _isRolling = true;

        Vector3 rollDirection = transform.forward;
        Vector3 targetPosition = transform.position + rollDirection * rollDistance;

        float timeElapsed = 0f;
        while (timeElapsed < rollDuration)
        {
            _rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, timeElapsed / rollDuration));

            float rollRotation = Mathf.Lerp(0f, 360f, timeElapsed / rollDuration);
            transform.Rotate(0f, rollRotation * Time.deltaTime * rollSpeed, 0f); 
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _rb.MovePosition(targetPosition);
        _isRolling = false;
    }
}
