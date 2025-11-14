using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damagePerTick = 5;           // dégâts par tick
    public float tickInterval = 0.5f;       // fréquence des dégâts

    private float nextTickTime = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") == false) return;

        if (Time.time >= nextTickTime)
        {
            var combat = other.GetComponent<PlayerCombat>();
            if (combat != null)
            {
                combat.TakeDamage(damagePerTick);
                nextTickTime = Time.time + tickInterval;
            }
        }
    }
}
