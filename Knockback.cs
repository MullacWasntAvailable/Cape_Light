using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockedBack {  get; private set; }
    private Rigidbody2D rb;
    [SerializeField] private float knockBackTime = .2f;

    private void  Awake()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    

    public void GetKnockedBack(Transform DamageSouce, float knockBackThrust)
    {
        GettingKnockedBack = true;
        Vector2 difference = (transform.position - DamageSouce.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockedBack = false;

    }
}

