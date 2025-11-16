using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    [Header("Stats du Boss")]
    public int maxHealth = 20;
    private int currentHealth;

    [Header("Loot & Effets")]
    public GameObject lootPrefab;        // L'objet � drop
    public GameObject deathEffectPrefab; // Fum�e
    public float despawnDelay = 0.2f;

    [Header("Fire Attack Manager")]
    public FireAttackManager fireAttackManager;

    [Header("Playerdetector")]
    public GameObject playerdetector;

    public Transform player;

    private bool isDead = false;

    public int factId = 6;

    void Start()
    {
        currentHealth = maxHealth;

        if (player == null) //si on ne le trouve pas on le cherche
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

    }

    public void TakeDamage(int amount)
    {
        BossUI.Instance.UpdateBossHealth(currentHealth, maxHealth);
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        BossUI.Instance.HideBossBar();



        // Stoppe toutes les attaques de feu
        if (fireAttackManager != null)
        {
            fireAttackManager.StopFireCombat();
        }

        // Effet visuel de fum�e
        if (deathEffectPrefab != null)
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        // Drop de l'objet
        if (lootPrefab != null)
            Instantiate(lootPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        // Destruction du boss apr�s un l�ger d�lai
        Destroy(gameObject, despawnDelay);
        Destroy(playerdetector, despawnDelay);

        var playerInventory = player.GetComponent<PlayerInventory>();
        playerInventory.AddFacts(factId);
    }
}
