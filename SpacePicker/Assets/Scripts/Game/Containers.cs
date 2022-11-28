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
    private RecycleZone[] recycleZones;

    private Transform[] containers = new Transform[3] { null, null, null};

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
        Vector3 initPosition = new Vector3(containesrsXPositions[number], 40, 0);

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
        StartCoroutine(MoveContainerCoroutine(number, false));
    }

    public int[] GetCounts()
    {
        return new int[2] { caughtCount, missedCount };
    }

    private IEnumerator MoveContainerCoroutine(int number, bool isMovingToStart)
    {
        Transform container = containers[number];
        Transform containerTopPart = container.transform.Find("ContainerTopPart");
        if (isMovingToStart)
        {
            // Moving of containers to central position and openingthem.
            StartCoroutine(MoveCoroutine(container, container.localPosition + new Vector3(0, -40, 0)));
            yield return new WaitForSecondsRealtime(2f);
            StartCoroutine(RotateTopPartCoroutine(containerTopPart, containerOpenedQuaternion));
        }
        else
        {
            // Closing of containers and counting of caught and missed trash.
            StartCoroutine(RotateTopPartCoroutine(containerTopPart, containerClosedQuaternion));
            yield return new WaitForSecondsRealtime(1f);
            recycleZones[number].enabled = !recycleZones[number].enabled;
            yield return new WaitForSecondsRealtime(0.5f);
            int[] counts = recycleZones[number].GetLocalCounts();
            caughtCount += counts[0];
            missedCount += counts[1];
            yield return new WaitForSecondsRealtime(0.5f);
            recycleZones[number].enabled = !recycleZones[number].enabled;

            // Moving of containers off the scene and destroing them.
            StartCoroutine(MoveCoroutine(container, container.localPosition + new Vector3(0, 0, -80)));
            yield return new WaitForSecondsRealtime(1f);
            StartCoroutine(MoveCoroutine(container, container.localPosition + new Vector3(0, -80, 0)));
            yield return new WaitForSecondsRealtime(1f);
            containers[number] = null;
            Destroy(container.gameObject);
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
