using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;

    public void Respawn()
    {
        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position;
        }
        else
        {
            Debug.LogWarning("There's no established Checkpoint");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();

            if (checkpoint != null && !checkpoint.IsActive())
            {
                currentCheckpoint = other.transform;
                checkpoint.Activate();

                Debug.Log("New Checkpoint activated on: " + other.name);
            }
        }
    }
}
