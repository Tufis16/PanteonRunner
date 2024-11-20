using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;

    // Lists to hold all colliders, rigidbodies, and joints
    public List<Collider> colliders = new List<Collider>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public List<Joint> joints = new List<Joint>();

    public List<MonoBehaviour> scripts = new List<MonoBehaviour>();

    private void Start()
    {
        animator = GetComponent<Animator>();

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogWarning("CharacterController component is not found on this GameObject.");
        }

        // Find all colliders, rigidbodies, and joints in child objects and add them to the lists
        foreach (Collider col in gameObject.GetComponentsInChildren<Collider>())
        {
            if (col.transform != gameObject.transform) // Exclude parent
            {
                colliders.Add(col);
            }
        }

        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rigidbodies.Add(rb);

            //Setting all rb's kinematic true because for smooth ragdoll transition
            rb.isKinematic = true;
        }

        foreach (Joint joint in gameObject.GetComponentsInChildren<Joint>())
        {
            joints.Add(joint);
        }

        foreach (MonoBehaviour script in gameObject.GetComponents<MonoBehaviour>())
        {
            scripts.Add(script);
        }
    }

    public void Die()
    {
        //Gameover when player die.
        RagdollTransition();
    }

    private void RagdollTransition()
    {
        animator.enabled = false;
        characterController.enabled = false;

        // Enable or disable all colliders, rigidbodies, and joints based on the state parameter
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = false;
        }

        foreach (var item_Script in scripts)
        {
            item_Script.enabled = false;
        }
    }
}
