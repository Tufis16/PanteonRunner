using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RagdollController))]
public class RagdollControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector
        DrawDefaultInspector();

        // Get a reference to the RagdollController script
        RagdollController ragdollController = (RagdollController)target;

        //Giving space for inspector window, more beautiful and you can see better
        EditorGUILayout.Space();

        if (GUILayout.Button("Kill Character"))
        {
            ragdollController.Die();
        }

        EditorGUILayout.Space();

        // Buttons to enable/disable all Colliders
        if (GUILayout.Button("Turn All Colliders Enable"))
        {
            SetCollidersEnabled(ragdollController, true);
        }
        
        if (GUILayout.Button("Turn All Colliders Disable"))
        {
            SetCollidersEnabled(ragdollController, false);
        }

        EditorGUILayout.Space();

        // Buttons to set all Rigidbodies as kinematic/non-kinematic
        if (GUILayout.Button("Turn All Rigidbodies Kinematic True"))
        {
            SetRigidbodiesKinematic(ragdollController, true);
        }

        if (GUILayout.Button("Turn All Rigidbodies Kinematic False"))
        {
            SetRigidbodiesKinematic(ragdollController, false);
        }

        EditorGUILayout.Space();

        // Buttons to set Rigidbody interpolation to true/false
        if (GUILayout.Button("Turn All Rigidbody Interpolation True"))
        {
            SetRigidbodiesInterpolation(ragdollController, RigidbodyInterpolation.Interpolate);
        }

        if (GUILayout.Button("Turn All Rigidbody Interpolation False"))
        {
            SetRigidbodiesInterpolation(ragdollController, RigidbodyInterpolation.None);
        }

        EditorGUILayout.Space();

        // Buttons to enable/disable gravity on all Rigidbodies
        if (GUILayout.Button("Turn All Rigidbody Gravity True"))
        {
            SetRigidbodiesGravity(ragdollController, true);
        }

        if (GUILayout.Button("Turn All Rigidbody Gravity False"))
        {
            SetRigidbodiesGravity(ragdollController, false);
        }
    }

    private void SetCollidersEnabled(RagdollController controller, bool enabled)
    {
        foreach (Collider col in controller.GetComponentsInChildren<Collider>())
        {
            col.enabled = enabled;
        }
    }

    private void SetRigidbodiesKinematic(RagdollController controller, bool isKinematic)
    {
        foreach (Rigidbody rb in controller.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = isKinematic;
        }
    }

    private void SetRigidbodiesInterpolation(RagdollController controller, RigidbodyInterpolation interpolation)
    {
        foreach (Rigidbody rb in controller.GetComponentsInChildren<Rigidbody>())
        {
            rb.interpolation = interpolation;
        }
    }

    private void SetRigidbodiesGravity(RagdollController controller, bool useGravity)
    {
        foreach (Rigidbody rb in controller.GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = useGravity;
        }
    }
}
