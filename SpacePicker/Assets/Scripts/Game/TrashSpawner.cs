using UnityEngine;

/// <summary>
/// Class that provide spawning of trash on belt.
/// </summary>
public class TrashSpawner : MonoBehaviour
{
    [SerializeField]
    private bool isSpawning;
    [SerializeField]
    private float respawnTime;
    private float timer;
    
    [Space]
    [SerializeField]
    private GameObject[] trashObjects;

    /// <summary>
    /// On/Off trash spawning proccess.
    /// </summary>
    public void SetSpawning(bool value)
    {
        isSpawning = value;
    }

    /// <summary>
    /// Setting up respawn cooldown.
    /// </summary>
    public void SetRespawnTime(float value)
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
            transform.position + new Vector3(0, 0, (Random.value * 2 - 1) / 5f),
            Quaternion.Euler(0, Random.Range(-180, 180), 0),
            transform);
    }
}
