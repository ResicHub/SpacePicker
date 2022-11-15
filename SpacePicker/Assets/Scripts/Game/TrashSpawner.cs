using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField]
    private float respawnTime;
    private float timer;
    public bool isSpawning = false;

    [SerializeField]
    private GameObject[] trashObjects;

    /// <summary>
    /// Setting up respawn cooldown.
    /// </summary>
    public void SetRespawn(float value)
    {
        respawnTime = value;
        timer = value;
    }

    private void FixedUpdate()
    {
        if (isSpawning)
        {
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
            {
                Spawn();
                timer = respawnTime;
            }
        }
    }

    private void Spawn()
    {
        Instantiate(
            trashObjects[Random.Range(0, trashObjects.Length)],
            transform.position + new Vector3((Random.value * 2 - 1) / 2, 0, 0),
            Quaternion.Euler(0, Random.Range(-180, 180), 0),
            transform);
    }
}
