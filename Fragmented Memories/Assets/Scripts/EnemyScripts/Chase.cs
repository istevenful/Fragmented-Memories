using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * This uses the A* pathfinding algorithm to chase the character within the givin field of nodes.
 * Changing the width and depth of the grid determines how far the AI will chase the player.
 * Followed along with "2D PATHFINDING - Enemy AI" by Brackeys on Youtube, changing code as needed
 */

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class Chase : MonoBehaviour
{
    [SerializeField] GameObject Target;
    // How many times to update path.
    [SerializeField] float UpdateRate = 2f;
    private Seeker Seeker;
    private GameObject MainCamera;

    // Calculated path.
    public Path Path;

    [SerializeField] float ChaseSpeed = 1.0f;

    private bool PathIsEnded = false;

    private float NextWaypointDistance = 3f;

    private int DestinationPoint = 0;

    // Start is called before the first frame update
    void Start()
    { 
        this.MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        this.Seeker = this.GetComponent<Seeker>();

        if (this.Target == null)
        {
            Debug.LogError("No target found");
            return;
        }

        Seeker.StartPath(transform.position, Target.transform.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    void Update()
    {
        // CheckDirection();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.Path == null)
        {
            return;
        }
        CheckPlayerDistance();
        if (TargetReached())
        {
            StopChasing();
        }
        else
        {
            Chasing();
        }           
    }

    private bool TargetReached()
    {
        return this.DestinationPoint >= this.Path.vectorPath.Count;
    }

    private void StopChasing()
    {
        if (!this.PathIsEnded)
        {
            // Stops less abruptly.
            /*
            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetFloat("Velocity", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / Random.Range(2f, 6f)));
            */

            this.PathIsEnded = true;
        }
    }

    private void Chasing()
    {
        this.PathIsEnded = false;
        float Step = this.ChaseSpeed * Time.deltaTime;
        this.gameObject.transform.position = Vector2.MoveTowards(transform.position, Path.vectorPath[this.DestinationPoint], Step);

        var Dist = Vector3.Distance(this.transform.position, this.Path.vectorPath[this.DestinationPoint]);
        if (Dist < this.NextWaypointDistance)
        {
            this.DestinationPoint++;  
        }
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            this.Path = p;
            this.DestinationPoint = 0;
        }      
    }

    private void CheckPlayerDistance()
    {
        bool PlayerIsWithinRange = (this.MainCamera.transform.position.x > this.transform.position.x + 10f) && (this.MainCamera.transform.position.x < this.transform.position.x);
        Debug.Log(PlayerIsWithinRange);
        if (!PlayerIsWithinRange)
        {
            UpdateBehavior();
        }
    }

    private void UpdateBehavior()
    {
        Debug.Log("Not working");
        var ChaseScript = this.gameObject.GetComponent<Chase>();
        var PatrolScript = this.gameObject.GetComponent<Patrol>();
        
        PatrolScript.enabled = true;
        ChaseScript.enabled = false;
        
    }

    IEnumerator UpdatePath()
    {
        Debug.Log(this.Target.transform.position.x);
        Seeker.StartPath(transform.position, this.Target.transform.position, OnPathComplete);

        yield return new WaitForSeconds(this.UpdateRate);
        StartCoroutine(UpdatePath());
    }

    // TODO: Setup complicated logic if necessary.
    private void CheckDirection()
    {
        var Direction = Path.vectorPath[this.DestinationPoint].x - this.gameObject.transform.position.x;
        if (Direction <= 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
