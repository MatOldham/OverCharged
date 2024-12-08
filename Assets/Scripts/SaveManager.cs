using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using TMPro;


public class SaveManager : MonoBehaviour
{

    [SerializeField] GameObject[] buttons;
    [SerializeField] TextMeshProUGUI[] buttonTexts;
    // Start is called before the first frame update
    void Start()
    {
        string saveName, sceneName;
        for(int i = 0; i < 3; i++){
            saveName = i + ".txt";
            NDSaveLoad.SetFileName(saveName);
            if(!NDSaveLoad.checkFileExists()){
                buttons[i].GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,1f);
                buttonTexts[i].text = "Save Slot " + i;
            }else{
                buttons[i].GetComponent<Image>().color = new Color(1f,1f,1f,1f);
                NDSaveLoad.LoadFromFile();
                sceneName = NDSaveLoad.GetData("currentLevel", "Starting Room");
                buttonTexts[i].text = "Save Slot " + i + " - " + sceneName;
            }
        }
    }

    public void StartGame(int i){
        string saveName, sceneName;
        saveName = i + ".txt";
        print(saveName);
        NDSaveLoad.SetFileName(saveName);
        if(NDSaveLoad.checkFileExists()){
            NDSaveLoad.LoadFromFile();
            sceneName = NDSaveLoad.GetData("currentLevel", "Starting Room");
            print(sceneName);
            Time.timeScale = 1;
            PlayerPrefs.SetInt("loadedFromSave",1);
            PlayerPrefs.SetInt("saveSlot",i);
            SceneManager.LoadScene(sceneName);
        }else{
            sceneName = "Starting Room";
            print(sceneName + "no file");
            Time.timeScale = 1;
            PlayerPrefs.SetInt("loadedFromSave",0);
            PlayerPrefs.SetInt("saveSlot",i);
            SceneManager.LoadScene(sceneName);  
        }
    }
}
