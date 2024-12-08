using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BombExplosion : MonoBehaviour
{
    [SerializeField] public int chargeLevel = 0;
    [SerializeField] public bool polarity = false;
    GameObject bombObject;
    // Start is called before the first frame update
    void Start()
    {
        
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.9919137f, 1f, 0.9009434f, 0.4705882f);
        transform.localScale = new Vector3(3,3,3);
        if(Math.Abs(chargeLevel) == 2){
            transform.localScale = new Vector3(5,5,5);
        }
        if(!polarity) this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.6084906f, 0.7882353f, 1f, 0.4705882f);

        Destroy(this.gameObject, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("BombWall")){
            Destroy(other.gameObject);
        }

        
        if(other.CompareTag("Battery")){
            //do chargey stuff
            print(chargeLevel);
            other.GetComponent<Battery>().charge(chargeLevel);
        }


    }
}
