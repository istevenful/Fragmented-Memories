using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Script goes onto the player.
public class EnemyHealth : MonoBehaviour
{
    public int health = 3;
    void Update()
    {
        if (health <= 0)
        {
            if(this.gameObject != null)
            {
                DestroyImmediate(this.gameObject);
            }
           
        }
    }

}
