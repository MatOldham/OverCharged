using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileSpawner : MonoBehaviour
{
    [SerializeField] Player playerObject;
    [SerializeField] GameObject ranged;
    [SerializeField] GameObject melee;
    [SerializeField] GameObject bomb;
    [SerializeField] AudioSource audioSource;
    GameObject currentBomb;
    private float parentOffsetX = 0f, parentOffsetY = 0f, parentOffsetZ = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRanged(){
        //Vector3 spawnPos = new Vector3(playerObject.transform.localPosition.x + .3125, playerObject.transform.localPosition.y - 0.125, playerObject.transform.localPosition.z);
        Transform parent = transform.parent.parent;
        
        if(parent != null){
            Transform parentParent = parent.parent;
            parentOffsetX = parent.localPosition.x + parentParent.localPosition.x;
            parentOffsetY = parent.localPosition.y + parentParent.localPosition.y;
            print(parentOffsetY);

            parentOffsetZ = parent.localPosition.z + parentParent.localPosition.z;
        }

        if(playerObject.facing){
            Vector3 spawnPos = new Vector3(playerObject.transform.localPosition.x + 0.525f + parentOffsetX, playerObject.transform.localPosition.y - 0.10f + parentOffsetY, playerObject.transform.localPosition.z + parentOffsetZ);
            var proj = Instantiate(ranged, spawnPos, Quaternion.Euler(0, 0, -90)); //right
           
        }else{
            Vector3 spawnPos = new Vector3(playerObject.transform.localPosition.x - 0.525f + parentOffsetX, playerObject.transform.localPosition.y - 0.10f + parentOffsetY, playerObject.transform.localPosition.z + parentOffsetZ);
            var proj = Instantiate(ranged, spawnPos, Quaternion.Euler(0, 0, 90));
          
        }
        audioSource.pitch = UnityEngine.Random.Range(0.8f,1.2f);
        audioSource.Play();
        parentOffsetX = 0f;
        parentOffsetY = 0f;
        parentOffsetZ = 0f;
    }

    public void SpawnMelee(){

    }

    public void placeBomb(int vertical){
        Transform parent = transform.parent.parent;
        
        if(parent != null){
            Transform parentParent = parent.parent;
            parentOffsetX = parent.localPosition.x + parentParent.localPosition.x;
            parentOffsetY = parent.localPosition.y + parentParent.localPosition.y;
            parentOffsetZ = parent.localPosition.z + parentParent.localPosition.z;
        }

        float offset = playerObject.facing == true ? 0.5f : -0.5f;
        if(playerObject.bombCount > 0 && playerObject.currentBombs < playerObject.maxConcurrentBombs){
            Vector3 spawnPos = new Vector3(playerObject.transform.localPosition.x + offset + parentOffsetX, playerObject.transform.localPosition.y + parentOffsetY, playerObject.transform.localPosition.z + parentOffsetZ);
            playerObject.currentBombObj = Instantiate(bomb, spawnPos, Quaternion.identity);
            playerObject.currentBombObj.GetComponent<Bomb>().charge(playerObject.polarity);
            playerObject.currentBombObj.GetComponent<Bomb>().upSpeed = vertical;
            playerObject.bombCount--;
            playerObject.currentBombs++;
        }
        parentOffsetX = 0f;
        parentOffsetY = 0f;
        parentOffsetZ = 0f;
    }

    
}
