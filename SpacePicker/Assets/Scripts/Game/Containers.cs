using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains parameters and methods for creating and moving containers.
/// </summary>
public class Containers : MonoBehaviour
{
    [SerializeField]
    private float[] containesrsXPositions;
    [SerializeField]
    private GameObject containerPrefab;
    private const float speed = 0.3f;

    [SerializeField]
    private BoxCollider[] recycleZonesColliders;
    [SerializeField]
    private Transform[] containers = new Transform[3] { null, null, null };
    private bool[] canRecreateContainer = new bool[] { true, true, true };

    private static int caughtCount;
    private static int missedCount;

    private readonly Quaternion containerClosedQuaternion = Quaternion.Euler(-90, 0, 0);
    private readonly Quaternion containerOpenedQuaternion = Quaternion.Euler(-180, 0, 0);

    private void Start()
    {
        caughtCount = 0;
        missedCount = 0;
    }

    /// <summary>
    /// Creates containers with number.
    /// </summary>
    /// <param name="number"></param>
    public void CreateContainer(int number)
    {
        canRecreateContainer[number] = false;
        Vector3 initPosition = new Vector3(containesrsXPositions[number], 9.75f, 0);

        GameObject container = Instantiate(
            containerPrefab,
            transform.localPosition + initPosition,
            Quaternion.identity,
            transform);

        containers[number] = container.transform;

        StartCoroutine(MoveContainerCoroutine(number, true));
    }

    /// <summary>
    /// Cleans up container and moves it out.
    /// </summary>
    /// <param name="number"></param>
    public void SendContainerToRecycle(int number)
    {
        if (canRecreateContainer[number] && containers[number] != null)
        {
            canRecreateContainer[number] = false;
            StartCoroutine(MoveContainerCoroutine(number, false));
        }
    }

    public int[] GetCounts()
    {
        return new int[2] { caughtCount, missedCount };
    }

    private IEnumerator MoveContainerCoroutine(int number, bool isMovingToStart)
    {
        Transform container = containers[number];
        Transform containerTopPart = container.Find("ContainerTopPart");
        if (isMovingToStart)
        {
            // Moving of containers to central position and openingthem.
            StartCoroutine(MoveCoroutine(container, container.localPosition + new Vector3(0, -10, 0)));
            yield return new WaitForSecondsRealtime(2f);
            StartCoroutine(RotateTopPartCoroutine(containerTopPart, containerOpenedQuaternion));
            yield return new WaitForSecondsRealtime(2f);
            canRecreateContainer[number] = true;
        }
        else
        {
            // Closing of containers and counting of caught and missed trash.
            StartCoroutine(RotateTopPartCoroutine(containerTopPart, containerClosedQuaternion));
            yield return new WaitForSecondsRealtime(2f);
            recycleZonesColliders[number].enabled = !recycleZonesColliders[number].enabled;
            yield return new WaitForSecondsRealtime(0.1f);
            int[] counts = recycleZonesColliders[number].gameObject.GetComponent<RecycleZone>().GetLocalCounts();
            caughtCount += counts[0];
            missedCount += counts[1];
            recycleZonesColliders[number].enabled = !recycleZonesColliders[number].enabled;

            // Create new empty container and move it to central position.
            CreateContainer(number);

            // Moving of containers off the scene and destroing them.
            StartCoroutine(MoveCoroutine(container, container.localPosition + new Vector3(0, 0, -20)));
            yield return new WaitForSecondsRealtime(1f);
            StartCoroutine(MoveCoroutine(container, container.localPosition + new Vector3(0, -20, 0)));
            Destroy(container.gameObject, 3f);
        }
    }

    private IEnumerator MoveCoroutine(Transform container, Vector3 targetPosition)
    {
        Vector3 startPosition = container.transform.localPosition;
        float step = 0;
        while (step < 1)
        {
            step += Time.fixedDeltaTime * speed;
            container.localPosition = Vector3.Lerp(startPosition, targetPosition, step);
            yield return null;
        }
    }

    private IEnumerator RotateTopPartCoroutine(Transform topPart, Quaternion targetRotation)
    {
        Quaternion startRotation = topPart.rotation;
        float step = 0;
        while (step < 1)
        {
            step += Time.fixedDeltaTime * speed;
            topPart.localRotation = Quaternion.Lerp(startRotation, targetRotation, step);
            yield return null;
        }
    }
}
