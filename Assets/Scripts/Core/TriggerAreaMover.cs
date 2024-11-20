using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaMover : MonoBehaviour
{
    public enum PushDirection
    {
        PushingRight, 
        PushingLeft
    }

    [Header("Movement Settings")]
    public PushDirection pushDirection;
    public float speed = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Character"))
        {
            //Determine the direction of push
            Vector3 pushDirectionVector = Vector3.zero;

            //Set the push direction based on the enum value
            switch (pushDirection)
            {
                case PushDirection.PushingRight:
                    pushDirectionVector = transform.right; // Push to the right (object's right)
                    break;
                case PushDirection.PushingLeft:
                    pushDirectionVector = -transform.right; // Push to the left (object's left)
                    break;
            }

            //Getting CharacterController component of the player
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                //Apply force to CharacterController
                characterController.Move(pushDirectionVector * speed * Time.deltaTime);
            }
        }
    }
}
