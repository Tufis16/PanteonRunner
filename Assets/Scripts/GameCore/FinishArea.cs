using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    public Transform finishWalkPoint;

    private void OnTriggerEnter(Collider other)
    {
        //Destroy when character hit
        if (other.CompareTag("Player"))
        {        
            //Triggering finishj screen and movement
            GameManager.Instance.GameFinishLine();
        }
    }
}
