using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Script goes onto the player.
public class Health : MonoBehaviour
{
    public int health;
    public int NumberofHeart;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite EmptyHeart;


    public static bool PlayerisDead = false;
    [SerializeField] private GameObject GameOverMenuUI;
    [SerializeField] private int MenuIndex;
    void Update()
    {
        if(health > NumberofHeart)
        {
            health = NumberofHeart;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            //decideds which heart is full or empty
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }
            //show how much hearts to display. (Player can gain more hearts in some way).
            if (i < NumberofHeart)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }


        if (health <= 0)
        {
            GameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void LoadMenu()
    {
        GameOverMenuUI.SetActive(false);
        SceneManager.LoadScene(MenuIndex);
        
        //completely freeze the game.
        Time.timeScale = 1f;

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
