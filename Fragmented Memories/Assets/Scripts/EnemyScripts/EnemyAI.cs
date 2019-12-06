using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float ScanRange = 7f;

    private Chase Chasing;
    private Patrol Patroling;
    private Fly Flying;
    private GameObject Target;
    
    private float XAxisModifier = 1000f;
    private float Health = 100f;
    // private Attack Attacking;
    // Use this for initialization
    void Start()
    {
        if (this.CompareTag("E1"))
        {
            this.Patroling = this.gameObject.GetComponent<Patrol>();
        }
        else if (this.CompareTag("F1"))
        {
            this.Flying = this.gameObject.GetComponent<Fly>();
        }
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
        }
        else if (TargetIsInNearby())
        {
            Debug.Log("Chasing");
            this.Chasing.Chasing();
        }
        else
        {
            // Debug.Log("Patroling");
            if (this.CompareTag("E1"))
            {
                this.Patroling.Patroling();
            }
            else if (this.CompareTag("F1"))
            {
                this.Flying.Flying();
            }
        }
    }

    private bool TargetIsInRange()
    {
        return Vector2.Distance(this.gameObject.transform.position, this.Target.transform.position) < 1f;
    }

    private bool TargetIsInNearby()
    {
        return Vector2.Distance(this.gameObject.transform.position, this.Target.transform.position) < 5f;
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
