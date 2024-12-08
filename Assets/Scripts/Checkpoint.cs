using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] public Sprite[] monitorSprites;
    [SerializeField] bool isSavePoint;
    [SerializeField] AudioSource audioSource;
    
    public int bombCount;
    private GameObject tempCheckpoint;
    private bool hasSaved = false, loadedFromSave = false;



    // Start is called before the first frame update
    void Start()
    {   
        
        if(isSavePoint){    
            string saveSlot = PlayerPrefs.GetInt("saveSlot",0) + ".txt";
            loadedFromSave = (PlayerPrefs.GetInt("loadedFromSave",0) != 0);
            if(loadedFromSave){
                NDSaveLoad.SetFileName(saveSlot);
                NDSaveLoad.LoadFromFile();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            if(isSavePoint && !hasSaved && !loadedFromSave){
                hasSaved = true;
                string saveSlot = PlayerPrefs.GetInt("saveSlot",0) + ".txt";
                NDSaveLoad.SetFileName(saveSlot);
                NDSaveLoad.SaveDataDict("currentLevel",SceneManager.GetActiveScene().name);
                other.GetComponent<Player>().bombCount = bombCount;
                NDSaveLoad.Flush();
            }
            else if(loadedFromSave){
                other.GetComponent<Player>().bombCount = bombCount;
            }
            else{
                bombCount = other.GetComponent<Player>().bombCount; 
                if(other.GetComponent<Player>().currentBombObj != null) bombCount++;
                
            }
            tempCheckpoint = other.GetComponent<Player>().checkpoint;
            other.GetComponent<Player>().checkpoint = this.gameObject;

            disable();
            audioSource.pitch = UnityEngine.Random.Range(0.5f,0.6f);
            audioSource.Play();
            if(tempCheckpoint != null) {
                tempCheckpoint.GetComponent<Checkpoint>().reEnable();
                tempCheckpoint = null;
            }
            
        }

    }
    public void disable(){
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = monitorSprites[0];
    }
    public void reEnable(){
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = monitorSprites[1];

    }
}
