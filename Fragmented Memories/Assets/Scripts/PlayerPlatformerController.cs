using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    // ADSR
    [SerializeField] private float MaxSpeed = 10.0f;
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
    private float InputDirection = 0.0f;
    private enum Phase { Attack, Decay, Sustain, Release, None };
    private Phase CurrentPhase;
    private float prevHorizontialAxisInput = 0f;

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
        //Debug.Log(velocity);
        return velocity;
    }

    //public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private float jumpWindow = 0.5f;
    private bool canAirJump = true;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        this.prevHorizontialAxisInput = 0f;
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        // Activate ADSR

        // GetButtonDown(Right)
        if (this.prevHorizontialAxisInput == 0f && Input.GetAxis("Horizontal") > 0)
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
        if (this.prevHorizontialAxisInput == 0f && Input.GetAxis("Horizontal") < 0)
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

        this.jumpWindow -= Time.deltaTime;

        // Still in air 
        if(Input.GetButtonDown("Jump") && !grounded)
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

        // Forgiven jump
        if(grounded && this.jumpWindow > 0f)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else
        {
            // Normal jump
            if (Input.GetButton("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / this.MaxSpeed);

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
}