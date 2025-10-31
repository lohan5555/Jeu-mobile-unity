using UnityEngine;

public class ChampiHeadHitbox : MonoBehaviour
{
    public int damage = 1;
    private Enemy enemy;

    void Awake()
    {
        enemy = FindAnyObjectByType<Enemy>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touched " + other);
            PlayerCombat player = other.GetComponent<PlayerCombat>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
