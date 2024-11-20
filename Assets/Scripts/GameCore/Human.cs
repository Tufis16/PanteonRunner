using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [Header("Player Settings")]
    public Animator animator;
    public CharacterController characterController;

    [Header("Move Settings")]
    public float moveSpeed = 15f;
    public float boundary = 5.55f;

    [Header("Health Settings")]
    public float health = 1f;
    public bool isAlive = true;
    public bool canMove = true;

    [Header("Ragdoll Components")]
    //Ragdoll components
    public List<Collider> colliders = new List<Collider>();
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();

    private void Start()
    {
        RagdollCollidersGetting();
        RagdollRigidbodiesGetting();

        //Disable ragdoll physics
        SetRagdollState(false);
    }

    private void RagdollRigidbodiesGetting()
    {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            if (rb.gameObject != this.gameObject)
            {
                rigidbodies.Add(rb);
            }
        }
    }

    private void RagdollCollidersGetting()
    {
        // Populate lists with all child colliders and rigidbodies
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            if (col.gameObject != this.gameObject)
            {
                colliders.Add(col);
            }
        }
    }

    private void Update()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            transform.position += Vector3.down * 9.8f * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            health = 0;
        }
    }

    public virtual void Die()
    {
        isAlive = false;
        canMove = false;
        Debug.Log("The human has died.");

        //Disable animator when died
        if (animator != null)
        {
            animator.enabled = false;
        }

        SetRagdollState(true);
    }

    //Enable ragdoll
    private void SetRagdollState(bool enabled)
    {
        foreach (Collider col in colliders)
        {
            col.enabled = enabled;
        }

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !enabled;
        }
    }

    //Collision Collide detection in here
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            Damage damageScript = hit.gameObject.GetComponent<Damage>();
            TakeDamage(damageScript.damageValue);            
        }
    }
}
