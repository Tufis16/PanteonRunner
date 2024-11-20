using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation Axes")]
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;

    [Header("Rotation Speed")]
    public float speed = 10f;

    // CharacterController reference
    public CharacterController characterController;

    // Platform's initial position
    private Vector3 initialPosition;

    void Start()
    {
        // Save the initial position of the platform to compensate for rotation
        initialPosition = transform.position;
    }

    void Update()
    {
        // Rotation vector
        Vector3 rotation = new Vector3(
            rotateX ? speed * Time.deltaTime : 0,
            rotateY ? speed * Time.deltaTime : 0,
            rotateZ ? speed * Time.deltaTime : 0
        );

        // Apply rotation to the platform
        transform.Rotate(rotation);
    }
}
