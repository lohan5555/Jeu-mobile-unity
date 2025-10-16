using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public int health = 20;
    public WeaponHitbox weaponHitbox;
    private bool isInvulnerable = false;
    public float invulnerabilityTime = 1.5f;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void EnableHitbox()
    {
        weaponHitbox.EnableHitbox();
    }

    public void DisableHitbox()
    {
        weaponHitbox.DisableHitbox();
    }
    

    public void TakeDamage(int damage)
    {

        if (isInvulnerable) return;

        health -= damage;
        Debug.Log("Enemy took damage, health = " + health);

        if (health <= 0)
        {
            Debug.Log("Player Die");
            animator.SetTrigger("Die");
        }
        else
        {
            Debug.Log("Player Hit");
            animator.SetTrigger("Hit");
        }

        StartCoroutine(InvulnerabilityCooldown());

    }
    private IEnumerator InvulnerabilityCooldown()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }
}
