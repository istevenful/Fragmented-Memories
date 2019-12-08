using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadNewScene : MonoBehaviour
{
    [SerializeField] private int LoadingSceneNumber;
    public Slider slider;
    private float number = 0;
    public Text percentage;
    void Update()
    {
        if (number < 100)
        {
            float multiplier = Random.Range(0f, .7f);
            number += Time.deltaTime * multiplier;
            slider.value = number;
            percentage.text = (int)(number * 100) + "%";

        }
        if (slider.value > .99)
        {
            Debug.Log(slider.value);
            SceneManager.LoadScene(LoadingSceneNumber);
        }
    }

}
