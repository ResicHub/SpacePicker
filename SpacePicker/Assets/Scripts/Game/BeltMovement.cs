using UnityEngine;

public class BeltMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private const float speedMultyplier = 50f;
    [SerializeField]
    private bool isMoving = true;

    /// <summary>
    /// Setting belt movement speed.
    /// </summary>
    public void SetSpeed(float value)
    {
        movementSpeed = value;
    }

    /// <summary>
    /// On/Off belt movement.
    /// </summary>
    public void Switch(bool value)
    {
        isMoving = value;
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            foreach (Transform roller in transform)
            {
                roller.Rotate(Vector3.up * movementSpeed * speedMultyplier * Time.deltaTime);
            }
        }
    }
}
