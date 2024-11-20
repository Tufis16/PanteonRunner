using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageValue = 1f;

    private void OnTriggerEnter(Collider other)
    {
        //Destroy when character hit
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Die();
        }else if (other.CompareTag("Character"))
        {
            Opponent opponent = other.gameObject.GetComponent<Opponent>();
            opponent.Die();
        }
    }
}
