using UnityEngine;

public class ChampiHeadHitbox : MonoBehaviour
{
       private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touched " + other);
            PlayerCombat player = other.GetComponent<PlayerCombat>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}
