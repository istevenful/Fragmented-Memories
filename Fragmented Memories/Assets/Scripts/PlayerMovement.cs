using System.Collections;
using System.Collections.Generic;
//using Player.Attack;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ADSR
    [SerializeField] private float AttackDuration = 0.5f;
    [SerializeField] private AnimationCurve Attack;
    [SerializeField] private float DecayDuration = 0.25f;
    [SerializeField] private AnimationCurve Decay;
    [SerializeField] private float SustainDuration = 100.0f;
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
    [SerializeField] private float jumpTakeOffSpeed = 2f;
    private Rigidbody2D rb { get; set; }

    // Jump forgiveness set to 0.5s
    [SerializeField] private float jumpWindow = 0.25f;

    // Air jump flag
    private bool canAirJump = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Attack
    [SerializeField] private GameObject attabox;
    private Vector3 flipVector = new Vector3(1.5f, 0, 0);
    //private IPlayerAttack normalAttack;
    private float attackDuration = 0.0f;

    // Health
    private bool isDead = false;
    // Player can only take damge once per this amount
    [SerializeField] private float damageProtection = 1.0f;
    private float damageProtectionTimer = 0.0f;

    // Ground check
    [SerializeField] private Transform groundCheck;
    float groundRadius = 0.2f;
    private bool grounded;
    public LayerMask whatIsGround;
    private int numAttack = 0;
    private float delay = 0;
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
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        this.prevHorizontialAxisInput = 0f;
        this.GetComponent<Rigidbody2D>().freezeRotation = true;
        this.rb = this.GetComponent<Rigidbody2D>();

        // Add command for attack
        this.attabox.SetActive(false);

        this.animator.SetBool("Dead", isDead);
        this.attackDuration = 0f;
        this.grounded = false;
        this.animator.SetBool("Attack", false);
    }

    private void Update()
    {
        //HandleInput();
        ComputeVelocity();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        this.attabox.SetActive(false);
        this.animator.SetBool("Attack", false);
        this.GetComponent<AudioSource>().enabled = false;
    }
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.LeftControl) && !this.isDead)
        {
            Debug.Log(delay);

            if (delay == 0)
            {
                delay += Time.deltaTime;
                numAttack = 1;
                this.attabox.SetActive(true);
                this.animator.SetBool("Attack", true);
                this.GetComponent<AudioSource>().enabled = true;
                StartCoroutine(Wait());
                numAttack = 0;
            }
        }

        if (delay != 0 && delay < 2)
        {
            delay += Time.deltaTime;
        }
        else if (delay >= 2)
        {
            delay = 0;
        }
        this.damageProtectionTimer -= Time.deltaTime;
    }

    // Called every Update()
    private void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (!this.isDead)
        {
            animator.SetBool("Ground", grounded);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x) / this.MaxSpeed);
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
        /*if (Input.GetButtonDown("Jump") && !grounded && !this.isDead)
        {
            this.jumpWindow = 0.5f;

            // Air jump
            if (canAirJump)
            {
                Debug.Log("air jump");
                rb.AddForce(new Vector2(0, jumpTakeOffSpeed * 0.5f));
                canAirJump = false;
            }
        }

        // Reset air jump
        if (grounded)
        {
            canAirJump = true;
        }

        // Forgiving jump
        /*if (grounded && this.jumpWindow > 0f && !this.isDead)
        {
            Debug.Log("Forgiving jump");
            rb.AddForce(new Vector2(0, jumpTakeOffSpeed * 2f));
            this.jumpWindow = 0f;
        }*/
        if (grounded && this.jumpWindow <= 0f && !this.isDead)
        {
            // Normal jump
            if (Input.GetButtonDown("Jump") && grounded && !this.isDead)
            {
                Debug.Log("normal jump");
                rb.AddForce(new Vector2(0, jumpTakeOffSpeed));
            }
            //else if (Input.GetButtonUp("Jump") && !this.isDead)
            //{
            //    if (rb.velocity.y > 0)
            //    {
            //        rb.velocity.y = velocity.y * 0.5f;
            //    }
            //}
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            attabox.transform.position = attabox.transform.position + (flipVector *= -1f);
        }


        if (grounded)
        {
            rb.velocity = new Vector2(move.x * this.ADSREnvelope() * this.MaxSpeed, rb.velocity.y);
        }
        else
        {
            // make it drag in air
            rb.velocity = new Vector2(move.x * this.ADSREnvelope() * this.MaxSpeed * 0.75f, rb.velocity.y);
        }
    }

    IEnumerator MyCoroutine(Collider2D collision)
    {
        this.gameObject.GetComponent<Health>().health--;
        if (collision.gameObject != null)
        {

            GameObject Enemy = collision.gameObject;
            Animator anim = Enemy.GetComponentInChildren<Animator>();
            yield return new WaitForSeconds(1f);
            if (anim != null)
            {
                anim.SetBool("Attack", false);
                Enemy.GetComponent<AudioSource>().enabled = false;
            }

            // Decreament health by enemy attack value

        }

    }
    // Collision with enemy
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision happen");
        var R = Random.Range(0, 3);
        if (collision.gameObject.tag == "Enemy" && this.damageProtectionTimer < 0.0f && collision.gameObject != null && R == 1)
        {
            GameObject Enemy = collision.gameObject;
            Animator anim = Enemy.GetComponentInChildren<Animator>();
            anim.SetBool("Attack", true);
            Enemy.GetComponent<AudioSource>().enabled = true;
            StartCoroutine(MyCoroutine(collision));
            Debug.Log("Damage");


            if (this.gameObject.GetComponent<Health>().health <= 0)
            {
                this.isDead = true;
                this.animator.SetBool("Dead", isDead);
            }

            // Reset damge protection timer
            this.damageProtectionTimer = this.damageProtection;

            // Knock back effect
            var attackDirection = (this.transform.position.x < collision.transform.position.x) ? 1 : -1;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5);
        }
    }
}
