using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float speed = 16f;
    [SerializeField] Vector3 projectileVector = new Vector3(0,1,0);
    [SerializeField] public bool polarity = false;    
    [SerializeField] Sprite[] projSprite;

    GameObject playerObject;
    // Start is called before the first frame update
    void Start()
    {   
        
        playerObject = GameObject.Find("Player");
        polarity = playerObject.GetComponent<Player>().polarity;
        if(polarity){
            gameObject.GetComponent<SpriteRenderer>().sprite = projSprite[0];
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().sprite = projSprite[1];
        }

        Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        projectileMove(projectileVector, speed);
    }

    public void projectileMove(Vector3 projectileVector, float speed){    
        transform.localPosition += Quaternion.Euler(0,0,this.transform.rotation.eulerAngles.z)  * projectileVector * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other){
        print(other.tag);
        if(other.CompareTag("Player")) return;
        if(other.CompareTag("Checkpoint")) return;
        if(other.CompareTag("Laser")) return;
        if(other.CompareTag("Bomb")){
            //do chargey stuff
            var bombScript = (Bomb)other.GetComponent<Bomb>();
            bombScript.charge(polarity);
            Destroy(this.gameObject);
            return;
        }

        if(other.CompareTag("Battery")){
            var batteryScript = (Battery)other.GetComponent<Battery>();
            if(polarity){
                batteryScript.charge(1);
            }
            else{
                batteryScript.charge(-1);
            }
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject);



    }
}
