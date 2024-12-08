using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void StartGame(){
        SceneManager.LoadScene("Room001");
        Time.timeScale = 1;
        
    }

    public void StartMenu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame(){
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
