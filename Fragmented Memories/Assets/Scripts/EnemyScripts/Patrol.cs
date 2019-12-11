using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * This uses the basic unity tutorial code for having an agent patrol between points.
 * To setup patrol points, create two empty objects in the world a set distance apart.
 * Drag those points into the "Points" Component on this script.
 */


public class Patrol : MonoBehaviour
{
    private Transform[] Points;

    private EnemyAI EnemyBehavior;
    private bool Initialized = false;

    private int DestinationPoint = 0;
    private bool MoveToNextPoint = true;

    public void Patroling()
    {
        if (!this.Initialized)
        {
            this.Initialized = true;
            this.EnemyBehavior = this.gameObject.GetComponent<EnemyAI>();
            this.Points = this.EnemyBehavior.Points;
        }
        // Returns if no points have been set up.
        if (this.Points.Length == 0)
        {
            return;
        }
        // If pause is wanted between Points, PauseBeforeNextPoint is called and the next point is updated.
        if (PointReached() && this.MoveToNextPoint)
        {
            StartCoroutine(PauseBeforeNextPoint());
        }
        if (this.MoveToNextPoint)
        {
            this.EnemyBehavior.CheckDirection(Points[this.DestinationPoint].transform.position.x);
            this.EnemyBehavior.MoveTowardsPoint(Points[this.DestinationPoint].transform.position.x);
        }
    }

    IEnumerator PauseBeforeNextPoint()
    {
        this.MoveToNextPoint = false;
        this.EnemyBehavior.StopMoving();
        this.DestinationPoint = (this.DestinationPoint + 1) % this.Points.Length;
        yield return new WaitForSeconds(this.EnemyBehavior.PauseTime);
        this.MoveToNextPoint = true;
    }

    private bool PointReached()
    {
        return Vector2.Distance(this.Points[this.DestinationPoint].position, this.gameObject.transform.position) < 1f;
    }
}
