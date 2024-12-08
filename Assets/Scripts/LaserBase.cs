using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBase : MonoBehaviour
{
    [SerializeField] public int chargeLevel = 0;
    [SerializeField] public bool pulsing = false;
    [SerializeField] public float pulsingTime = 2.0f, startTime = 0.0f;
    [SerializeField] public GameObject laser;
    [SerializeField] public bool invert;
    bool on = true;

    // Start is called before the first frame update
    void Start()
    {
        if(pulsing){
            InvokeRepeating("Toggle", startTime, pulsingTime);
        }else{

            if(!invert){
                if(chargeLevel != 0) on = false;
                else on = true;
            }else{
                if(chargeLevel != 0) on = true;
                else on = false;
            }
            if(on){
                laser.GetComponent<Collider2D>().enabled = true;
                laser.GetComponent<SpriteRenderer>().enabled = true;
            }else{
                laser.GetComponent<Collider2D>().enabled = false;
                laser.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }


    public void setCharge(int charge){
        chargeLevel = charge;
        
        if(!invert){
            if(chargeLevel != 0) on = false;
            else on = true;
        }else{
            if(chargeLevel != 0) on = true;
            else on = false;
        }
        if(on){
            laser.GetComponent<Collider2D>().enabled = true;
            laser.GetComponent<SpriteRenderer>().enabled = true;
        }else{
            laser.GetComponent<Collider2D>().enabled = false;
            laser.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    public void Toggle(){
        on = !on;
        if(on){
            laser.GetComponent<Collider2D>().enabled = true;
            laser.GetComponent<SpriteRenderer>().enabled = true;
        }
        else{
            laser.GetComponent<Collider2D>().enabled = false;
            laser.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
