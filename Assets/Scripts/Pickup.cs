using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] public bool isHealth;
    [SerializeField] GameObject triangle;


    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            if(isHealth){
                other.GetComponent<Player>().chargeLevel++;
            }
            else{
                other.GetComponent<Player>().bombCount++;
            }
            triangle.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
