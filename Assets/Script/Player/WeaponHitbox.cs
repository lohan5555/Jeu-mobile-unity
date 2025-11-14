using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{

    private Collider hitbox;

    void Awake()
    {
        hitbox = GetComponent<Collider>();
        hitbox.enabled = false;
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Touched enemy!");
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1, transform.position);
            }
        }
        if (other.CompareTag("Boss"))
        {
            Debug.Log("Touched BOSS!");
            Boss boss = other.GetComponent<Boss>();
            if (boss != null)
                boss.TakeDamage(1);
        }
    }
}
