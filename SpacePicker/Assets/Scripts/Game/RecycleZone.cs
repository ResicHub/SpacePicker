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

    private BoxCollider collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }

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

    public void CleanContainer()
    {
        StartCoroutine(CleanContainerCoroutine());
    }

    private IEnumerator CleanContainerCoroutine()
    {
        collider.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        collider.enabled = false;
    }
}
