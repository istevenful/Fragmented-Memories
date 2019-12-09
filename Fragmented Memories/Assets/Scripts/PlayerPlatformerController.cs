﻿using System.Collections;
using System.Collections.Generic;
//using Player.Attack;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    // ADSR
    [SerializeField] private float AttackDuration = 0.5f;
    [SerializeField] private AnimationCurve Attack;
    [SerializeField] private float DecayDuration = 0.25f;
    [SerializeField] private AnimationCurve Decay;
    [SerializeField] private float SustainDuration = 5.0f;
    [SerializeField] private AnimationCurve Sustain;
    [SerializeField] private float ReleaseDuration = 0.25f;
    [SerializeField] private AnimationCurve Release;
    private float AttackTimer;
    private float DecayTimer;
    private float SustainTimer;
    private float ReleaseTimer;
    private enum Phase { Attack, Decay, Sustain, Release, None };
    private Phase CurrentPhase;
    private float prevHorizontialAxisInput = 0f;

    // Movement vars
    [SerializeField] private float MaxSpeed = 10.0f;
    [SerializeField] private float jumpTakeOffSpeed = 7;

    // Jump forgiveness set to 0.5s
    [SerializeField] private float jumpWindow = 0.25f;

    // Air jump flag
    private bool canAirJump = true;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Attack
    [SerializeField] private GameObject attabox;
    private Vector3 flipVector = new Vector3(2.6f, 0, 0);
    //private IPlayerAttack normalAttack;
    private float attackDuration = 0.5f;

    // Health
    private bool isDead = false;
    // Player can only take damge once per this amount
    [SerializeField] private float damageProtection= 1.0f;
    private float damageProtectionTimer = 0.0f;

    // ADSR implemetation
    // Taken for Dr.Mccoy's class demo project
    private void ResetTimers()
    {
        this.AttackTimer = 0.0f;
        this.DecayTimer = 0.0f;
        this.SustainTimer = 0.0f;
        this.ReleaseTimer = 0.0f;
    }

    float ADSREnvelope()
    {
        float velocity = 0.0f;

        if (Phase.Attack == this.CurrentPhase)
        {
            velocity = this.Attack.Evaluate(this.AttackTimer / this.AttackDuration);
            this.AttackTimer += Time.deltaTime;
            if (this.AttackTimer > this.AttackDuration)
            {
                this.CurrentPhase = Phase.Decay;
            }
        }
        else if (Phase.Decay == this.CurrentPhase)
        {
            velocity = this.Decay.Evaluate(this.DecayTimer / this.DecayDuration);
            this.DecayTimer += Time.deltaTime;
            if (this.DecayTimer > this.DecayDuration)
            {
                this.CurrentPhase = Phase.Sustain;
            }
        }
        else if (Phase.Sustain == this.CurrentPhase)
        {
            velocity = this.Sustain.Evaluate(this.SustainTimer / this.SustainDuration);
            this.SustainTimer += Time.deltaTime;
            if (this.SustainTimer > this.SustainDuration)
            {
                this.CurrentPhase = Phase.Release;
            }
        }
        else if (Phase.Release == this.CurrentPhase)
        {
            velocity = this.Release.Evaluate(this.ReleaseTimer / this.ReleaseDuration);
            this.ReleaseTimer += Time.deltaTime;
            if (this.ReleaseTimer > this.ReleaseDuration)
            {
                this.CurrentPhase = Phase.None;
            }
        }
        
        return velocity;
    }

   

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        this.prevHorizontialAxisInput = 0f;
        this.GetComponent<Rigidbody2D>().freezeRotation = true;

        // Add command for attack
        //this.gameObject.AddComponent<PlayerAttack>();
        //this.normalAttack = this.gameObject.GetComponent<PlayerAttack>();
        this.attabox.SetActive(false);
    }

    void Update()
    {
        // Call Update() from PhysicsObject
        base.Update();



        if (Input.GetKeyDown(KeyCode.LeftControl) && !this.isDead)
        {
            //this.normalAttack.Attack(this.gameObject);
            this.AttackDuration = 0.5f;
        }

        if (this.AttackDuration > 0)
        {
            this.attabox.SetActive(true);
            this.animator.SetBool("Attack", true);

            var contacts = new Collider2D[32];
            var hitSomething = false;

            this.attabox.gameObject.GetComponent<BoxCollider2D>().GetContacts(contacts);

            foreach(var col in contacts)
            {
                if(col != null && col.gameObject.tag == "Enemy")
                {
                    // Deal damage to enemy

                    hitSomething = true;
                }
            }

            if(hitSomething)
            {
                this.AttackDuration = 0;
            }
        }
        else
        {
            this.attabox.SetActive(false);
            this.animator.SetBool("Attack", false);
        }

        this.AttackDuration -= Time.deltaTime;
        this.damageProtectionTimer -= Time.deltaTime;
    }

    // Called every Update()
    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (!this.isDead)
        {
            animator.SetBool("Ground", grounded);
            animator.SetFloat("Speed", Mathf.Abs(velocity.x) / this.MaxSpeed);
            animator.SetFloat("vSpeed", this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }

        // Activate ADSR
        // GetButtonDown(Right)
        if (this.prevHorizontialAxisInput == 0.0f && Input.GetAxis("Horizontal") > 0.01f)
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
        }
        // GetButtonUp(Right)
        if (this.prevHorizontialAxisInput > 0 && Input.GetAxis("Horizontal") == 0)
        {
            this.CurrentPhase = Phase.Release;
        }

        // GetButtonDown(Left)
        if (this.prevHorizontialAxisInput == 0.0f && Input.GetAxis("Horizontal") < -0.01f)
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
        }
        // GetButtonUp(Left)
        if (this.prevHorizontialAxisInput < 0 && Input.GetAxis("Horizontal") == 0)
        {
            this.CurrentPhase = Phase.Release;
        }

        this.prevHorizontialAxisInput = Input.GetAxis("Horizontal");


        // Air jump
        this.jumpWindow -= Time.deltaTime;

        // Still in air 
        if(Input.GetButtonDown("Jump") && !grounded && !this.isDead)
        {
            this.jumpWindow = 0.5f;

            // Air jump
            if(canAirJump)
            {
                velocity.y += jumpTakeOffSpeed;
                canAirJump = false;
            }
        }

        // Reset air jump
        if(grounded)
        {
            canAirJump = true;
        }

        // Forgiving jump
        if(grounded && this.jumpWindow > 0f && !this.isDead)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else
        {
            // Normal jump
            if (Input.GetButton("Jump") && grounded && !this.isDead)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump") && !this.isDead)
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            attabox.transform.position = attabox.transform.position + (flipVector *= -1f);
        }

        
        if (grounded)
        {
            targetVelocity = move * this.ADSREnvelope() * this.MaxSpeed;
        }
        else
        {
            // make it drag in air
            targetVelocity = move * this.ADSREnvelope() * this.MaxSpeed * 0.75f;
        }

    }

    // Collision with enemy
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision happen");

        if (collision.gameObject.tag == "enemy" && this.damageProtectionTimer < 0.0f)
        {
            Debug.Log("Damage");

            // Decreament health by enemy attack value
            this.gameObject.GetComponent<Health>().health--;

            if(this.gameObject.GetComponent<Health>().health <= 0)
            {
                this.animator.SetBool("Dead", true);
            }

            // Reset damge protection timer
            this.damageProtectionTimer = this.damageProtection;

            // Knock back effect
            var attackDirection = (this.transform.position.x < collision.transform.position.x) ? 1 : -1;
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(attackDirection, 0, 0) * 5.0f);
        }
    }
}
