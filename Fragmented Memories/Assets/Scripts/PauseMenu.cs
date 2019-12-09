using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    //public so it is accessable to other scripts, static cause we do not want to reference to this script, just want to check if the game is paused.
    public static bool GameisPaused = false;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private int MenuIndex;



    //Check if the player hits the escape key, if game is already paused then resume, else pause.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameisPaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("RESUME");
        //turn off the Main menu UI.
        PauseMenuUI.SetActive(false);
        //completely freeze the game.
        Time.timeScale = 1f;
        GameisPaused = false;
    }
    //Oppisite of ResumeGame function.
    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(MenuIndex);
        ResumeGame();

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
