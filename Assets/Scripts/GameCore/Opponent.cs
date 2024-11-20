using UnityEngine;

public class Opponent : Human
{
    //AI-specific variables
    [Header("Opponent.cs Settings")]
    private float targetPositionX;
    private float changeDirectionTime;
    private float timeSinceLastChange;

    [SerializeField] private float minChangeDirectionTime = 0.1f; //Minimum random direction change
    [SerializeField] private float maxChangeDirectionTime = 1.25f; //Maximum random direction change

    private void Awake()
    {
        //Set random movement
        SetNewTargetPosition();
        changeDirectionTime = GetRandomChangeTime();
    }

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
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("AnimSpeed", moveSpeed);
    }

    //AI Movement calculations
    public void Move()
    {
        Vector3 forwardMove = Vector3.forward * moveSpeed * Time.deltaTime;

        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= changeDirectionTime)
        {
            SetNewTargetPosition();
            changeDirectionTime = GetRandomChangeTime();
            timeSinceLastChange = 0f;
        }

        //Calculate horizontal movement towards the target position
        float direction = Mathf.Sign(targetPositionX - characterController.transform.position.x);
        float horizontalMove = direction * moveSpeed * Time.deltaTime;

        //Clamping movement
        float newPositionX = characterController.transform.position.x + horizontalMove;
        if ((direction > 0 && newPositionX > targetPositionX) || (direction < 0 && newPositionX < targetPositionX))
        {
            newPositionX = targetPositionX;
        }

        Vector3 move = forwardMove + Vector3.right * (newPositionX - characterController.transform.position.x);
        characterController.Move(move);
    }

    private void SetNewTargetPosition()
    {
        targetPositionX = Random.Range(-boundary, boundary);
    }

    private float GetRandomChangeTime()
    {
        return Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
