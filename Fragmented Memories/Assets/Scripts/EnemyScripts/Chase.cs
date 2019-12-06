using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*
 * This uses the A* pathfinding algorithm to chase the character within the givin field of nodes.
 */

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class Chase : MonoBehaviour
{
    // How many times to update path.
    private GameObject Target;
    private Seeker Seeker;

    // Calculated path.
    public Path Path;

    [SerializeField] float ChaseSpeed = 1.0f;

    private bool PathIsEnded = false;

    private float NextWaypointDistance = 3f;

    private int DestinationPoint = 0;

    private bool UpdatingPath = false;


    public void Chasing()
    {
        if (!this.UpdatingPath)
        {
            this.Seeker = this.GetComponent<Seeker>();
            this.Target = GameObject.FindGameObjectWithTag("Player");
            this.UpdatingPath = true;
            StartCoroutine(UpdatePath());
        }

        if (this.Path == null)
        {
            return;
        }

        bool TargetReached = this.DestinationPoint >= this.Path.vectorPath.Count;
        if (!TargetReached)
        {
            CheckDirection();
            MoveTowardsTarget();
        }
        else
        {
            StopChasing();
        }
    }

    private void StopChasing()
    {
        if (!this.PathIsEnded)
        {
            // Stops less abruptly.
            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetFloat("Velocity", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / Random.Range(2f, 6f)));

            this.PathIsEnded = true;
        }
    }

    private void MoveTowardsTarget()
    {
        this.PathIsEnded = false;

        float Step = this.ChaseSpeed * Time.deltaTime;
        this.gameObject.transform.position = Vector2.MoveTowards(transform.position, Path.vectorPath[this.DestinationPoint], Step);

        var Dist = Vector2.Distance(this.transform.position, this.Path.vectorPath[this.DestinationPoint]);
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

    IEnumerator UpdatePath()
    {
        Seeker.StartPath(this.gameObject.transform.position, this.Target.transform.position, OnPathComplete);

        yield return new WaitForSeconds(2f);
        StartCoroutine(UpdatePath());
    }

    // Adding a fixed value to ensure logic works over negative and positive x axis.
    private void CheckDirection()
    {
        var Direction = (Path.vectorPath[this.DestinationPoint].x + 1000f) - (this.transform.position.x + 1000f);
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
