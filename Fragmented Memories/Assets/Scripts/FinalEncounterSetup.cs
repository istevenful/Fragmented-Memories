using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Determines what ending the player gets based on their final actions.
 */

public class FinalEncounterSetup : MonoBehaviour
{
    [SerializeField] private GameObject EnemyObject;
    [SerializeField] private GameObject GoodEndingUI;
    [SerializeField] private GameObject SadEndingUI;
    [SerializeField] private int MenuIndex;

    private void Update()
    {
        if (this.EnemyObject == null)
        {
            SadEnd();
        }
        else
        {
            // If object destroyed, can't run this.
            bool ObjectInPosition = this.EnemyObject.transform.position.x >= this.gameObject.transform.position.x && this.EnemyObject.transform.position.x <= this.gameObject.transform.position.x + 1;
            if (ObjectInPosition)
            {
                GoodEnd();
            }

        }       
    }

    void GoodEnd()
    {
        GoodEndingUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void SadEnd()
    {
        SadEndingUI.SetActive(true);
        Time.timeScale = 0f;
    }

}
