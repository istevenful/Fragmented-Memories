using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Script goes onto the player.
public class Health : MonoBehaviour
{
    public int health;
    public int NumberofHealth;
    public Image[] flowers;
    public Sprite AliveFlower;
    public Sprite DeadFlower;

    
    public static bool PlayerisDead = false;
    [SerializeField] private GameObject GameOverMenuUI;
    [SerializeField] private int MenuIndex;

    private void Start()
    {
        Debug.Log(this.name);
    }
    void Update()
    {
        if(health > NumberofHealth)
        {
            health = NumberofHealth;
        }
        for (int i = 0; i < flowers.Length; i++)
        {
            //decideds which heart is full or empty
            if (i < health)
            {
                flowers[i].sprite = AliveFlower;
            }
            else
            {
                flowers[i].sprite = DeadFlower;
            }
            //show how much hearts to display. (Player can gain more hearts in some way).
            if (i < NumberofHealth)
            {
                flowers[i].enabled = true;
            }
            else
            {
                flowers[i].enabled = false;
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
