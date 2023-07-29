using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HeartGlobe,
    }

    [SerializeField] PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float AccelartionRate = 0.2f;
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 moveDir;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance)
        {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += AccelartionRate;
        } else
        {
            moveDir = Vector3.zero;
            moveSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 

        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectPickUpType();
            Destroy(gameObject);
        }
    }

   private void DetectPickUpType ()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:

                EconomyManager.Instance.UpdateCurrentGold();

                break;


            case PickUpType.HeartGlobe:
                PlayerHealth.Instance.HealPlayer();
                Debug.Log("heal");
                break;

            case PickUpType.StaminaGlobe:

                break;
        }
    }

}
