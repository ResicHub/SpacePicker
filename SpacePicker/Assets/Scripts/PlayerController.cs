using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isSettingsActive = false;

    private float heightChangeSpeed = 0.5f;
    [SerializeField]
    private List<float> heightBorders;
    private float positionChangeSpeed = 0.5f;
    [SerializeField]
    private List<float> positionBorders;

    private void Update()
    {
        if (OVRInput.Get(OVRInput.Button.Any))
        {
            CheckInput();
        }
    }

    private void CheckInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            isSettingsActive = !isSettingsActive;
        }
        if (isSettingsActive)
        {
            // Height
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
            {
                if (transform.position.y < heightBorders[1])
                {
                    transform.position += heightChangeSpeed * Time.fixedDeltaTime * Vector3.up;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
            {
                if (transform.position.y > heightBorders[0])
                {
                    transform.position -= heightChangeSpeed * Time.fixedDeltaTime * Vector3.up;
                }
            }
            // Distance to table
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
            {
                if (transform.position.z < positionBorders[1])
                {
                    transform.position += positionChangeSpeed * Time.fixedDeltaTime * Vector3.forward;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
            {
                if (transform.position.z > positionBorders[0])
                {
                    transform.position -= positionChangeSpeed * Time.fixedDeltaTime * Vector3.forward;
                }
            }
        }
    }
}
