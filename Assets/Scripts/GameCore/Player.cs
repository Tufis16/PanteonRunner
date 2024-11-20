using UnityEngine;

public class Player : Human
{
    [Header("Player.cs Settings")]
    public float controlSensivity = 2f;
    public GameObject playerArrow;

    public bool finishLineMovement = false;

    private void Update()
    {
        if (isAlive)
        {
            UpdateAnimator();
        }

        if (canMove)
        {
            Move();
        }

        if (finishLineMovement)
        {
            FinishAreaMovement();
        }
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("AnimSpeed", moveSpeed);
    }

    //Player Movement calculation
    public void Move()
    {
        Vector3 forwardMove = Vector3.forward * moveSpeed * Time.deltaTime;

        float horizontalInput = 0;
        if (Input.GetMouseButton(0))
        {
            horizontalInput = Input.GetAxis("Mouse X") * controlSensivity * Time.deltaTime;
            float newPositionX = Mathf.Clamp(characterController.transform.position.x + horizontalInput, -boundary, boundary);
            horizontalInput = newPositionX - characterController.transform.position.x;
        }

        Vector3 move = forwardMove + Vector3.right * horizontalInput;
        characterController.Move(move);
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public override void Die()
    {
        base.Die();
        Destroy(playerArrow);

        //Triggering game over
        GameManager.Instance.GameOver();
    }

    float positionSmoothTime = 0.3f; 
    float rotationSmoothSpeed = 5f;  

    private Vector3 velocity = Vector3.zero;

    public void FinishAreaMovement()
    {
        //Disable player movement
        animator.applyRootMotion = false;
        canMove = false;
        moveSpeed = Mathf.Lerp(moveSpeed, 0 , 0.025f);

        transform.position = Vector3.SmoothDamp(transform.position, GameManager.Instance.finishArea.finishWalkPoint.position, ref velocity, positionSmoothTime);

        Quaternion targetRotation = Quaternion.LookRotation(GameManager.Instance.finishArea.finishWalkPoint.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothSpeed);
    }
}
