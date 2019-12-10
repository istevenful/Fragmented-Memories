using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    private Animator anim;
    bool dead = false;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        this.anim.SetBool("Dead", true);
        anim.SetFloat("Speed", 0);
        dead = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!dead)
            {
                this.anim.SetBool("Dead", true);
                anim.SetFloat("Speed", 0);
                dead = true;
            }
            else
            {
                anim.SetBool("Dead", false);
                dead = false;
            }
        }
    }
}
