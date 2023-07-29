using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float dashSpeed = 4f;
    private float startingMoveSpeed;

    private PlayerControl playerControl;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Knockback knockback;

    [SerializeField] private Transform weaponCollider;

    private bool attackButtonDown, isAttacking = false;
    private bool isDashing = false;

    [SerializeField] private float AttackCD = .5f;
    
    [SerializeField] private TrailRenderer myTrailRenderer;

   
    


    protected override void Awake()
    {
        base.Awake();

        playerControl = new PlayerControl();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
       
    }

   

    private void OnEnable()
    {
        playerControl.Enable();   
    }

   

    private void Update()
    {
        PlayerInput();
        Attack();
        
    }
    private void Start()
    {
        playerControl.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed;

        playerControl.Combat.Attack.started += _ => StartAttacking();
        playerControl.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void FixedUpdate()
    {
        Move();
        AdjustPlayerFacingDirection();
      
    }

    private void PlayerInput()
    { 
    movement = playerControl.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            StartCoroutine(AttackCDRoutine());
        }
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(AttackCD);
        isAttacking = false;
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    public void DoneAttacking()
    {
        weaponCollider.gameObject.SetActive(false);
    }


    private void Move()
    {
        if (knockback.GettingKnockedBack || PlayerHealth.Instance.isdead ) { return; }

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    

   

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x) 
        { 
            mySpriteRenderer.flipX = true;
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
           else 
        { 
            mySpriteRenderer.flipX = false;
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
}

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            myTrailRenderer.emitting = true;
            moveSpeed *= dashSpeed;
            StartCoroutine(EndDashRoutine());
        }
    }
    
    private IEnumerator EndDashRoutine()
    {
        float DashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(DashTime);

        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting=false;
        yield return new WaitForSeconds(dashCD);
        isDashing=false;
    }
}
  
