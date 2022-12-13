using UnityEngine;

/// <summary>
/// Contains method to count missed trash in lava pit.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class Lava : MonoBehaviour
{
    private int localMissedCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trash")
        {
            localMissedCount++;
            Destroy(other.gameObject);
        }
    }
}
