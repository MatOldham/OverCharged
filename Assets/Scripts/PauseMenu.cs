using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Player playerObject;
    [SerializeField] bool closedByDefault = true;
    [SerializeField] GameObject restartCheckpointButton;
    private GameObject checkpoint;
    private bool active = false;

    void Awake(){
        if(closedByDefault){
            CloseMenu();
            active = false;
        }
    }
    public void Pause(){
        checkpoint = playerObject.checkpoint;
        Time.timeScale = 0;
        if(checkpoint == null){
            restartCheckpointButton.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1f);
        }
        else{
            restartCheckpointButton.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        }
        OpenMenu();
    }
    public void Unpause(){
        if(!active) return;
        Time.timeScale = 1;
        CloseMenu();
    }
    public void OpenMenu(){
        active = true;
        GetComponent<Canvas>().enabled = true;
    }
    public void CloseMenu(){
        active = false;
        GetComponent<Canvas>().enabled = false;
    }
    public void StartMenu(){

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        CloseMenu();
    }
    public void RestartLevel(){
        PlayerPrefs.SetInt("loadedFromSave",1);
        Time.timeScale = 1;
        CloseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartCheckpoint(){
        if(!active) return;
        checkpoint = playerObject.checkpoint;
        if(checkpoint == null) return;
        playerObject.bombCount = checkpoint.GetComponent<Checkpoint>().bombCount;
        Destroy(playerObject.currentBombObj);
        playerObject.currentBombs = 0;
        playerObject.chargeLevel--;
        Time.timeScale = 1;
        CloseMenu();
        if(playerObject.chargeLevel <= 0) RestartLevel();
        else playerObject.transform.position = checkpoint.transform.position;
        
    }
}
