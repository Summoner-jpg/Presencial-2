using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] _movingPoints;
    [SerializeField] private float _moveSpeed;
    private int _nextPlatform = 1;
    private bool _platformOrder = true;

    private void Update()
    {
        if (_platformOrder && _nextPlatform + 1 >= _movingPoints.Length)
        {
            _platformOrder = false;
        }

        if (!_platformOrder && _nextPlatform <= 0)
        {
            _platformOrder = true;
        }

        if (Vector2.Distance(transform.position, _movingPoints[_nextPlatform].position) < 0.1f)
        {
            if (_platformOrder)
            {
                _nextPlatform += 1;
            }
            else
            {
                _nextPlatform -= 1;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, _movingPoints[_nextPlatform].position,
            _moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    other.transform.SetParent(this.transform);
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
