using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] public int chargeLevel = 0;
    [SerializeField] public bool polarity = false;
    [SerializeField] public Sprite[] bombSprite;
    [SerializeField] public GameObject blast;
    [SerializeField] AudioSource audioSource;
    private bool detonated = false;
    public int upSpeed = 0;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.up * upSpeed);
        gameObject.GetComponent<SpriteRenderer>().sprite = bombSprite[2];
    }

    // Update is called once per frame
    void Update()
    {
        updateSprite();
    }

    public void Detonate(){
        //should never have charge level of 0 if here
        if(detonated) return;
        detonated = true;
        Transform parent = transform.parent;
        Vector3 spawnPos;
        if(parent == null){
            spawnPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }else{
            Transform parentParent = parent.parent;
            spawnPos = new Vector3(transform.localPosition.x + parent.localPosition.x + parentParent.localPosition.x, transform.localPosition.y + parent.localPosition.y + parentParent.localPosition.y, transform.localPosition.z + parent.localPosition.z + parentParent.localPosition.z);
      
        }
       
        var explosion = Instantiate(blast, spawnPos, Quaternion.identity);
        var explosionScript = explosion.GetComponent<BombExplosion>();
        explosionScript.polarity = polarity;
        explosionScript.chargeLevel = chargeLevel;
        Destroy(this.gameObject, 1);
        this.gameObject.SetActive(false);
        
    }

    public void charge(bool polarity){
        if(polarity && chargeLevel < 2){
            chargeLevel += 1;
        }
        else if(!polarity && chargeLevel > -2){
            chargeLevel -= 1;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = bombSprite[chargeLevel + 2];
        if(chargeLevel > 0) this.polarity = true;
        else this.polarity = false;
    }
    public void updateSprite(){
        gameObject.GetComponent<SpriteRenderer>().sprite = bombSprite[chargeLevel + 2];
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlat")){
            audioSource.pitch = Random.Range(0.7f,1.0f);
            audioSource.Play();
        }
        
    }
    
}
