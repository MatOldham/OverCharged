using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] public int chargeLevel;
    [SerializeField] public float speed;
    [SerializeField] public float[] speeds;
    [SerializeField] public int startingPoint;
    [SerializeField] public Transform[] points;
    [SerializeField] public bool platFacing = false;
    [SerializeField] public bool vertical = false; //left false right true
    [SerializeField] public Sprite[] platSprite;
    private int i; //collisionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = platSprite[chargeLevel];
        speed = speeds[chargeLevel];
        print(speed);
        transform.position = points[startingPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.2f){
            
            i++;
            int prevI = i-1;
            if(i == points.Length){
                i = 0;
            }
            if(points[i].localPosition.x > points[prevI].localPosition.x){
                platFacing = true;
            }else if(points[i].localPosition.x < points[prevI].localPosition.x){
                platFacing = false;
            }else{
                vertical = true;
            }
            
        }   
        
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);


    }
    public void setCharge(int charge){
        chargeLevel = Math.Abs(charge);
        gameObject.GetComponent<SpriteRenderer>().sprite = platSprite[chargeLevel];
        speed = speeds[chargeLevel];
    }
    
    private void OnCollisionEnter2D(Collision2D collision){
        //collisionCount++;
       
        //if(collisionCount == 2) collision.transform.SetParent(transform);
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision){
        //if(collisionCount == 2){
        //    collisionCount = 0;
        //}
        collision.transform.SetParent(null);
    }

    
}
