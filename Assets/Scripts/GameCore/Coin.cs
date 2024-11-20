using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public float moveDuration = 1f;
    private bool isMoving = false;
    private Transform targetTransform;

    public int value = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            //Adding and updating coins
            GameManager.Instance.AddCoin(value);

            isMoving = true;

            //Disable collider from bad multiple triggers
            GetComponent<Collider>().enabled = false;

            //Setting null for world movement
            transform.SetParent(null);

            targetTransform = GameManager.Instance.coinMovePoint.transform;

            MoveToTarget();
        }
        else if (other.CompareTag("Character"))
        {
            Destroy(gameObject);
        }
    }

    private void MoveToTarget()
    {
        if (targetTransform != null)
        {
            transform.DOMove(targetTransform.position, moveDuration)
                .SetEase(Ease.InOutQuad) 
                .OnUpdate(CheckProximity) 
                .OnComplete(() =>
                {
                    transform.position = targetTransform.position;
                    Destroy(gameObject);
                });
        }
    }

    private void CheckProximity()
    {
        //Finish calculations
        if (Vector3.Distance(transform.position, targetTransform.position) < 0.05f)
        {
            transform.position = targetTransform.position;
            Destroy(gameObject);
        }
    }
}
