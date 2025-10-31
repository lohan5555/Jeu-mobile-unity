using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public int health = 100;
    public WeaponHitbox weaponHitbox;
    private bool isInvulnerable = false;
    public float invulnerabilityTime = 1.5f;
    private Animator animator;
    private PlayerInventory PlayerInventory;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            Debug.Log($"[PlayerCombat] Animator retrouvé automatiquement: {animator != null}");
        }
        else
        {
            Debug.Log($"animator ok: {animator}");
        }
    }

    void Update()
    {
        Debug.Log($"Animator active : {animator.enabled} | IsPlaying? {animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")}");
    }

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        PlayerInventory = GetComponentInParent<PlayerInventory>();
    }

    public void EnableHitbox()
    {
        weaponHitbox.EnableHitbox();
    }

    public void DisableHitbox()
    {
        weaponHitbox.DisableHitbox();
    }


    private int comboStep = 0;
    private float lastAttackTime = 0f;
    public float comboResetTime = 1f; // temps max entre deux attaques pour garder le combo
    public void OnAttack()
    {
        if (Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
        }

        comboStep++;
        lastAttackTime = Time.time;
        Debug.Log("Combot Step : " + comboStep);

        if (comboStep == 1)
        {
            animator.SetTrigger("Attack");
        }
        else if (comboStep == 2)
        {
            animator.SetTrigger("Attack2");
        }
        else if (comboStep == 3)
        {
            animator.SetTrigger("Attack3");
            comboStep = 0;
        }

    }
    

    public void TakeDamage(int damage)
    {

        if (isInvulnerable) return;

        health -= damage;
        Debug.Log("Enemy took damage, health = " + health);
        HealthSystem.Instance.TakeDamage(damage);

        if (health <= 0)
        {
            Debug.Log("Player Die");
            animator.SetTrigger("Die");
            StartCoroutine(RespawnCooldown());
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

    private IEnumerator RespawnCooldown()
    {
        animator.SetTrigger("Respawn");

        //on attant la fin de l'animation de mort
        yield return new WaitForSeconds(0.5f);
        
        //on replace le joueur
        var player = FindAnyObjectByType<PlayerCombat>()?.gameObject;
        SpawnPoint spawn = FindAnyObjectByType<SpawnPoint>();
        player.transform.position = player.transform.position;
        transform.rotation = spawn.transform.rotation;

        //on réinitialise ses stats
        health = 100;
        PlayerInventory.clear();

        //on recharge la scène
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
