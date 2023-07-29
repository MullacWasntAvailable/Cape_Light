using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject DeathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currantHealth;
    private Knockback knockback;
    private Flash flash;


    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }
    

    private void Start()
    {
        currantHealth = startingHealth;
       
    }

    private void Update()
    {
       
    }

    public void TakeDamage(int damage)
    {
        currantHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
  
    }
    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currantHealth <= 0 ) 
        {
            Instantiate(DeathVFXPrefab,transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
