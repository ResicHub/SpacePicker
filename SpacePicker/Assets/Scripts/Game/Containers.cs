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
    [SerializeField]
    private float speed;

    private readonly Quaternion containerClosedQuaternion = Quaternion.Euler(-90, 0, 0);
    private readonly Quaternion containerOpenedQuaternion = Quaternion.Euler(-180, 0, 0);

    private void Start()
    {
        CreateContainer(0);
        CreateContainer(1);
        CreateContainer(2);
    }

    /// <summary>
    /// Creates containers with number.
    /// </summary>
    /// <param name="number"></param>
    public void CreateContainer(int number)
    {
        Vector3 startPosition = new Vector3(containesrsXPositions[number], 40, 0);
        Vector3 targetPosition = new Vector3(containesrsXPositions[number], 0, 0);

        GameObject container = Instantiate(
            containerPrefab,
            transform.localPosition + startPosition,
            Quaternion.identity,
            transform);

        StartCoroutine(MoveContainerToBelt(container.transform, startPosition, targetPosition));
    }

    private IEnumerator MoveContainerToBelt(Transform container, Vector3 start, Vector3 target)
    {
        float step = 0;
        while (step < 1)
        {
            step += Time.fixedDeltaTime * speed;
            container.localPosition = Vector3.Lerp(start, target, step);
            yield return null;
        }

        Transform containerTopPart = container.Find("ContainerTopPart");
        StartCoroutine(OpenContainerCoroutine(containerTopPart));
    }

    private IEnumerator OpenContainerCoroutine(Transform containerTopPart)
    {
        float step = 0;
        while (step < 1)
        {
            step += Time.fixedDeltaTime * speed;
            containerTopPart.localRotation = Quaternion.Lerp(
                containerClosedQuaternion, 
                containerOpenedQuaternion, 
                step);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(CloseContainerCoroutine(containerTopPart));
    }
    private IEnumerator CloseContainerCoroutine(Transform containerTopPart)
    {
        float step = 0;
        while (step < 1)
        {
            step += Time.fixedDeltaTime * speed;
            containerTopPart.localRotation = Quaternion.Lerp(
                containerOpenedQuaternion,
                containerClosedQuaternion,
                step);
            yield return null;
        }
    }


}
