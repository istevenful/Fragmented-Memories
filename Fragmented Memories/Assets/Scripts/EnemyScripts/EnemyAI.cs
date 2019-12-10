using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform[] Points;
    public float PauseTime;

    [SerializeField] float PatrolSpeed = 1f;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask WhatIsGround;
    [SerializeField] float ScanRange = 5f;

    private Animator animator;
    private Rigidbody2D rigidbody2D;

    private Chase Chasing;
    private Patrol Patroling;
    private GameObject Target;

    private float GroundRadius = 0.2f;
    private bool Grounded = false;

    private float Health = 100f;
    private bool ChasePaused = true;
    // private Attack Attacking;
    // Use this for initialization
    void Start()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
        this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        this.rigidbody2D.freezeRotation = true;

        this.Patroling = this.gameObject.GetComponent<Patrol>();
        this.Chasing = this.gameObject.GetComponent<Chase>();
        this.Target = GameObject.FindGameObjectWithTag("Player");
        // this.Attacking = this.gameObject.GetComponent<Attack>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TargetIsInRange())
        {
            // Debug.Log("Attacking");
            StopMoving();
            // this.Attacking.Attacking();
        }
        else if (TargetIsWithinScanRange())
        {
            // Debug.Log("Chasing");
            this.ChasePaused = false;
        }
        else if (this.ChasePaused)
        {
            // Debug.Log("Patroling");
            this.Patroling.Patroling();
        }
        else if (!this.ChasePaused)
        {
            this.Chasing.Chasing();
        }
    }

    private bool TargetIsInRange()
    {
        // Debug.Log(Vector2.Distance(this.gameObject.transform.position, this.Target.transform.position));
        return Vector2.Distance(this.gameObject.transform.position, this.Target.transform.position) < 3f;
    }

    private bool TargetIsWithinScanRange()
    {
        return Vector2.Distance(this.gameObject.transform.position, this.Target.transform.position) < this.ScanRange;
    }

    public bool IsDead()
    {
        if (this.Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveTowardsPoint(float destinationPoint)
    {
        this.Grounded = Physics2D.OverlapCircle(this.GroundCheck.position, this.GroundRadius, this.WhatIsGround);
        this.animator.SetBool("Ground", this.Grounded);
        // Set the agent to go to the currently selected destination.
        float Step = this.PatrolSpeed * Time.deltaTime;
        var Destination = new Vector2(destinationPoint, 0f);
        this.gameObject.transform.position = Vector2.MoveTowards(transform.position, Destination, Step);
        this.animator.SetFloat("vSpeed", this.rigidbody2D.velocity.y);
        this.animator.SetFloat("Speed", Mathf.Abs(this.PatrolSpeed));
        // this.rigidbody2D.velocity = new Vector2(this.PatrolSpeed, this.rigidbody2D.velocity.y);
    }

    public void StopMoving()
    {
        // Animation looks weird if you try to slow down. Coroutine wasn't working.
        // this.animator.SetFloat("Speed", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / Random.Range(2f, 6f)));
        this.animator.SetFloat("Speed", 0f);
    }

    public void CheckDirection(float destinationPoint)
    {
        // Next position minus the current position gives vector with direction.
        float EnemyDirection = (destinationPoint + 1000f) - (this.gameObject.transform.position.x + 1000f);
        // Debug.Log(EnemyDirection);
        if (EnemyDirection <= 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

    }
}
