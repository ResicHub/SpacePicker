using UnityEngine;

/// <summary>
/// Contains category number of trash object.
/// </summary>
public class TrashObject : MonoBehaviour
{
    /// <summary>
    /// Returns category number.
    /// </summary>
    [SerializeField]
    public int Category;

    private void Start()
    {
        gameObject.tag = "Trash";
    }
}
