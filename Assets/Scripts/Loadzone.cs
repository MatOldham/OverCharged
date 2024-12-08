using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loadzone : MonoBehaviour
{

    [SerializeField] string nextScene;
    [SerializeField] int nextBombCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(!other.CompareTag("Player")) return; 
        PlayerPrefs.SetInt("bombCount", nextBombCount);
        PlayerPrefs.SetInt("loadedFromSave",0);
        SceneManager.LoadScene(nextScene);

    }
}
