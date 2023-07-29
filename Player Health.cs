using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth> { 

    public bool isdead {  get; private set; }

    [SerializeField] private int maxHealth = 5;

    private int currentHealth;
    private Slider healthSlider; 


    private Knockback knockback;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float DamageRecoveryTime = 1f;


    private bool canTakeDamage = true;


    private Flash flash;

    const string HEALTH_SLIDER_TEXT = "Health Slider";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    const string DUNGEON_TEXT = "Scene 1";

    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        isdead = false;
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAi enemy = other.gameObject.GetComponent<EnemyAi>();

        if (enemy && canTakeDamage)
        {
            TakeDamage(1);
            knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
        }
    }

    public void HealPlayer()
    {

        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    private void TakeDamage(int damageAmount)
    {
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(RecoveryTime());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 )
        {
            isdead |= true;
            currentHealth = 0;
            GetComponent< Animator > ().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        SceneManager.LoadScene(DUNGEON_TEXT);
    }


    
private IEnumerator RecoveryTime()
    {
        yield return new WaitForSeconds(DamageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
      if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
