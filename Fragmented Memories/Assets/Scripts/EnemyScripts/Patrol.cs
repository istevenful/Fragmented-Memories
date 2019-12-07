using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * This uses the basic unity tutorial code for having an agent patrol between points.
 * To setup patrol points, create two empty objects in the world a set distance apart
 * Drag those points into the "Points" Component on this script
 */


public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] Points;
    [SerializeField] float PatrolSpeed;
    [SerializeField] float PauseTime;
    private int DestinationPoint = 0;
    private bool MoveToNextPoint = true;

    public void Patroling()
    {
        // Returns if no points have been set up.
        if (this.Points.Length == 0)
        {
            return;
        }
        // If pause is wanted between Points, PauseBeforeNextPoint is called and the next point is updated.
        bool PointReached = Vector2.Distance(this.Points[this.DestinationPoint].position, this.gameObject.transform.position) < 1f;
        if (PointReached && this.MoveToNextPoint)
        {
            StartCoroutine(PauseBeforeNextPoint());
        }
        if (this.MoveToNextPoint)
        {
            CheckDirection();
            MoveTowardsPoint();
        }
    }

    IEnumerator PauseBeforeNextPoint()
    {
        this.MoveToNextPoint = false;
        this.DestinationPoint = (this.DestinationPoint + 1) % this.Points.Length;
        yield return new WaitForSeconds(this.PauseTime);
        this.MoveToNextPoint = true;
    }

    private void CheckDirection()
    {
        // Next position minus the current position gives vector with direction.
        var Direction = this.Points[this.DestinationPoint].position.x + 1000f - this.transform.position.x + 1000f;
        if (Direction <= 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void MoveTowardsPoint()
    {
        // Set the agent to go to the currently selected destination.
        float Step = this.PatrolSpeed * Time.deltaTime;
        this.gameObject.transform.position = Vector2.MoveTowards(transform.position, this.Points[this.DestinationPoint].position, Step);
    }
}
