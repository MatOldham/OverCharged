using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public int chargeLevel = 0;
    [SerializeField] public bool invert;
    bool open = false;
    [SerializeField] public Sprite[] doorSprite;
    // Start is called before the first frame update
    void Start()
    {
        if(!invert){
            if(chargeLevel != 0) open = false;
            else open = true;
        }else{
            if(chargeLevel != 0) open = true;
            else open = false;
        }
        
        if(open){
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite[1];
        }else{
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setCharge(int charge){
        chargeLevel = charge;

        if(!invert){
            if(chargeLevel != 0) open = false;
            else open = true;
        }else{
            if(chargeLevel != 0) open = true;
            else open = false;
        }
        
        if(open){
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite[1];
        }else{
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = doorSprite[0];
        }

    }
    
}
