using UnityEngine;

/// <summary>
/// Contains information and methods ror recycle zone.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class RecycleZone : MonoBehaviour
{
    [SerializeField]
    private readonly int category;

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
}
