using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TrashSpawner spawner;
    [SerializeField]
    private BeltMovement belt;
    [SerializeField]
    private Containers containers;

    private bool gameOn;

    private void Start()
    {
        gameOn = false;
        StartCoroutine(GameStartCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ContainerButtonPressed(0);
            ContainerButtonPressed(1);
            ContainerButtonPressed(2);
        }
    }

    private IEnumerator GameStartCoroutine()
    {
        OVRScreenFade.instance.FadeIn();
        yield return new WaitForSecondsRealtime(2f);
        containers.CreateContainer(0);
        containers.CreateContainer(1);
        containers.CreateContainer(2);

        yield return new WaitForSecondsRealtime(2f);

        gameOn = true;
        spawner.SetSpawning(true);
        belt.SetMovement(true);
    }

    /// <summary>
    /// Sends container with number to recycle,
    /// </summary>
    /// <param name="number"></param>
    public void ContainerButtonPressed(int number)
    {
        containers.SendContainerToRecycle(number);
    }
}
