using UnityEngine;

/// <summary>
/// Ñlass that provides rotation of all belt cylinders.
/// </summary>
public class BeltMovement : MonoBehaviour
{
    [SerializeField]
    private bool isMoving;
    [SerializeField]
    private float movementSpeed;
    private const float speedMultiplier = 100f;

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
    public void SetMovement(bool value)
    {
        isMoving = value;
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            foreach (Transform roller in transform)
            {
                Rigidbody rollerRB = roller.GetComponent<Rigidbody>();
                Quaternion deltaRotation = Quaternion.Euler(Vector3.up * movementSpeed * speedMultiplier * Time.fixedDeltaTime);
                rollerRB.MoveRotation(roller.rotation * deltaRotation);
            }
        }
    }
}
