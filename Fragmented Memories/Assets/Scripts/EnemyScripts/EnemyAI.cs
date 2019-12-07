using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float ScanRange = 10f;
    [SerializeField] float PauseChaseTime = 3.0f;
    [SerializeField] bool PauseChaseAfterAttacking = false;

    private Chase Chasing;
    private Patrol Patroling;
    private GameObject Target;
    
    private float Health = 100f;
    private bool ChasePaused = false;
    // private Attack Attacking;
    // Use this for initialization
    void Start()
    {
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
            Debug.Log("Attacking");
            // this.Attacking.Attacking();
            if (this.PauseChaseAfterAttacking)
            {
                StartCoroutine(PauseChase());
            }
        }
        else if (TargetIsWithinScanRange() && !this.ChasePaused)
        {
            Debug.Log("Chasing");
            this.Chasing.Chasing();
        }
        else
        {
            // Debug.Log("Patroling");
            this.Patroling.Patroling();
        }
    }

    private bool TargetIsInRange()
    {
        return Vector2.Distance(this.gameObject.transform.position, this.Target.transform.position) < 1f;
    }

    private bool TargetIsWithinScanRange()
    {
        return Vector2.Distance(this.gameObject.transform.position, this.Target.transform.position) < this.ScanRange;
    }

    IEnumerator PauseChase()
    {
        this.ChasePaused = true;
        yield return new WaitForSeconds(this.PauseChaseTime);
        this.ChasePaused = false;
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
}
