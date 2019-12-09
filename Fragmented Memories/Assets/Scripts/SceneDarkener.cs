using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneDarkener : MonoBehaviour
{
    public Image darkScreen;
    [SerializeField] private float Alpha;
    [SerializeField] private float MaxAlpha;
    void Update()
    {
        if(Alpha < MaxAlpha)
        {
            Alpha += .0002f;

            darkScreen.color = new Color(darkScreen.color.r, darkScreen.color.g, darkScreen.color.b, Alpha);
        }

        
    }
}
