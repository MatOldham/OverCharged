using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] public int chargeLevel = 0;
    [SerializeField] public bool polarity = false;
    [SerializeField] public Sprite[] batterySprite;
    [SerializeField] public GameObject[] connectedObjects;
    [SerializeField] AudioSource audioSource;

    
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = batterySprite[chargeLevel+2];
        updateCharges();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void charge(int incomingCharge){
        chargeLevel += incomingCharge;
        if(chargeLevel > 2) chargeLevel = 2;
        if(chargeLevel < -2) chargeLevel = -2;
        gameObject.GetComponent<SpriteRenderer>().sprite = batterySprite[chargeLevel + 2];

        if(chargeLevel > 0) this.polarity = true;
        else this.polarity = false;
        audioSource.pitch = UnityEngine.Random.Range(0.8f,1.2f);
        audioSource.Play();
        //update all attached objects
        updateCharges();
    }

    private void updateCharges(){

        int arrayLength = connectedObjects.Length;
        if(arrayLength == 0) return;
        for(int i = 0; i <arrayLength; i++){
            if(connectedObjects[i].CompareTag("Door")){
                connectedObjects[i].GetComponent<Door>().setCharge(chargeLevel);
            }
            if(connectedObjects[i].CompareTag("MovingPlat")){
                connectedObjects[i].GetComponent<MovingPlatform>().setCharge(chargeLevel);
            }
            if(connectedObjects[i].CompareTag("Laser")){
                connectedObjects[i].GetComponent<LaserBase>().setCharge(chargeLevel);
            }
        }

    }
}

