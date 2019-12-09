using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette : MonoBehaviour
{
    private GameObject MainCamera;

    void Start()
    {
        this.MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(this.MainCamera.transform.position.x, this.MainCamera.transform.position.y, 0);
    }
}
