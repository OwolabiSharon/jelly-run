using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;
    public GameObject mainMenu;
    public GameObject gamePlay;

    //public bool isDone;
   // public bool isLeadDone;

   // string[] text = { "I bet you cant beat my highscore", "The traps are more of a problem than the fire", "You can actually run over those mace traps","The trick is to know when to slow down" };
    
    

    private void Start() {
        HighScoreText.text = "HighScore: " + PlayerPrefs.GetFloat("highscore");   
    }
    
    public void PlayGame()
    {
        characterMovement.instance.isPlaying = true;
        mainMenu.SetActive(false);
        //gamePlay.SetActive(true);
        //isDone = true;
    }

    public void death()
    {
        characterMovement.instance.isPlaying = false;
        gamePlay.SetActive(false);
        mainMenu.SetActive(true);
        
        //isDone = true;
    }

    // public void LeaderBoard()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    // }

    public void QuitGame ()
    {
        Application.Quit();
    }
}
