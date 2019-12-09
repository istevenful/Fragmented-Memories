using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    private GameObject Target;
    private EnemyAI EnemyBehavior;

    private bool Initialize = false;

    private bool TargetReached = false;

    public void Chasing()
    {
        if (!this.Initialize)
        {
            this.Initialize = true;
            this.EnemyBehavior = this.gameObject.GetComponent<EnemyAI>();
            this.Target = GameObject.FindGameObjectWithTag("Player");

        }

        if (!this.TargetReached)
        {
            this.EnemyBehavior.CheckDirection(this.Target.transform.position.x);
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        this.EnemyBehavior.MoveTowardsPoint(this.Target.transform.position.x);
        var Dist = Vector2.Distance(this.transform.position, this.Target.transform.position);
    }
}
