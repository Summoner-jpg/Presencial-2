using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
        player.transform.position = transform.position;
    }
}

