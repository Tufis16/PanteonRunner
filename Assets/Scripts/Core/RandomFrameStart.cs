using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFrameStart : MonoBehaviour {

    //This sciprt starts animations from random frame/time
    private Animator animator;

    [System.Obsolete]
    void OnEnable() {
        // animator.Update(Random.value);
        animator = GetComponent<Animator>();

        animator.ForceStateNormalizedTime(Random.Range(0.0f, 1.0f));

        Destroy(this);
    }
}
