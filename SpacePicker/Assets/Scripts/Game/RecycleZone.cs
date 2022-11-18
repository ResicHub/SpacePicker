using UnityEngine;

/// <summary>
/// Contains information and methods ror recycle zone.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class RecycleZone : MonoBehaviour
{
    [SerializeField]
    private readonly int category;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Trash")
        {
            if (category == other.gameObject.GetComponent<TrashObject>().Category)
            {
                // Create event for catch.
            }
            else
            {
                // Create event for miss.
            }
            Destroy(other.gameObject);
        }
    }
}
