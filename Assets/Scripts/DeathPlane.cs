using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private GameObject checkpoint, pauseMenu;
    [SerializeField] AudioSource audioSource;
   
    void Start(){
        pauseMenu = GameObject.Find("PauseMenu");
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            StartCoroutine(deathWait(other));
        }
    }
    IEnumerator deathWait(Collider2D other){
        audioSource.pitch = UnityEngine.Random.Range(2f,3f);
        audioSource.Play();
        Time.timeScale = 0.05f;
        
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        checkpoint = other.GetComponent<Player>().checkpoint;
        other.GetComponent<Player>().bombCount = checkpoint.GetComponent<Checkpoint>().bombCount;
        Destroy(other.GetComponent<Player>().currentBombObj);
        other.GetComponent<Player>().currentBombs = 0;
        other.GetComponent<Player>().chargeLevel--;
        if(other.GetComponent<Player>().chargeLevel <= 0) pauseMenu.GetComponent<PauseMenu>().RestartLevel();
        else other.transform.position = checkpoint.transform.position;
    }
}


