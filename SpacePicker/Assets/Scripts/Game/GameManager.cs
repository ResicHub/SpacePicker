using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TrashSpawner spawner;
    [SerializeField]
    private BeltMovement belt;

    private bool gameOn;
    private int level;

    private void Start()
    {
        gameOn = false;
        level = 1;

        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        OVRScreenFade.instance.FadeIn();
        yield return new WaitForSecondsRealtime(4);

        gameOn = true;
        spawner.SetSpawning(true);
        belt.SetMovement(true);
    }
}
