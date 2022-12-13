using System.Collections;
using UnityEngine;

/// <summary>
/// Contains information and methods ror recycle zone.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class RecycleZone : MonoBehaviour
{
    [SerializeField]
    private int category;

    private int localCaughtCount;
    private int localMissedCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trash")
        {
            if (category == other.gameObject.GetComponent<TrashObject>().Category)
            {
                localCaughtCount++;
            }
            else
            {
                localMissedCount++;
            }
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// Sends data of caught and missed count to GameManager.
    /// </summary>
    public int[] GetLocalCounts()
    {
        int caughtCount = localCaughtCount;
        int missedCount = localMissedCount;
        localCaughtCount = 0;
        localMissedCount = 0;
        return new int[2] { caughtCount, missedCount };
    }
}
