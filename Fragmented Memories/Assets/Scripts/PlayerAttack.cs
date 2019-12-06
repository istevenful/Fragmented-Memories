using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Attack
{
    public class PlayerAttack : MonoBehaviour, IPlayerAttack
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Attack(GameObject gameObject)
        {
            Debug.Log("Normal Attack!");
        }
    }
}
