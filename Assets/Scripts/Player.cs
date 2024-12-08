using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] public int chargeLevel = 3; //health
    [SerializeField] public bool polarity = true;
    [SerializeField] Sprite[] playerSprite;
    
    [SerializeField] float jumpSpeed = 8, walkSpeed = 10;
    [SerializeField] public int bombCount = 2, maxConcurrentBombs = 1, currentBombs = 0;
    [SerializeField] AudioSource audioSource, bombSource;
    public GameObject currentBombObj;
    Rigidbody2D rb;
    public GameObject checkpoint = null;

    public bool isGrounded = true, facing = true, platform = false;// platVertical = false, platFacing = false;
    //private float platSpeed = 0;
    //private bool jumped = false;
    

    void Awake(){
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(polarity){
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite[0];
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite[1];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate(){
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlat")){
            isGrounded = true;
            audioSource.pitch = Random.Range(0.7f,1.0f);
            audioSource.Play();
            //jumped = false;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground") ){
            isGrounded = false;
        }
        if(collision.gameObject.CompareTag("MovingPlat")){
            isGrounded = false;
            platform = false;
            
            /*
            platSpeed = collision.gameObject.GetComponent<MovingPlatform>().speed;
            platFacing = collision.gameObject.GetComponent<MovingPlatform>().platFacing;
            platVertical = collision.gameObject.GetComponent<MovingPlatform>().vertical;
            

            if(platform && !platVertical && jumped){
                print("gaming");
                Vector3 vector;
                if(facing){
                    if(platFacing){
                        vector =(Vector3.right * platSpeed * 50);
                    }else{
                        vector =(Vector3.left * platSpeed * 20);
                    }
                }else{
                    if(platFacing){
                        vector =(Vector3.right * platSpeed * 20);
                    }else{
                        vector =(Vector3.left * platSpeed * 50);
                    }
                }
                rb.AddForce(vector);
            }
            */
        }
    }

    public void Move(Vector3 vector){
        //rb.velocity = (vector * walkSpeed);
        transform.localPosition += vector * walkSpeed * Time.deltaTime;
    }
    public void Jump(){
        if(!isGrounded) return;
        Vector3 vector = (Vector3.up * jumpSpeed); // + (new Vector3(rb.velocity.x, 0,0) * walkSpeed);
        //jumped = true;
        
        rb.AddForce(vector);
    }

    public void changePolarity(){
        polarity = !polarity;
        if(polarity){
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite[0];
        }
        else{
            gameObject.GetComponent<SpriteRenderer>().sprite = playerSprite[1];
        }
    }
    public void flipSprite(bool flip){
        gameObject.GetComponent<SpriteRenderer>().flipX = flip;
        facing = !flip;
    }

    public void retrieveBomb(){
        if(currentBombs == 1){
            Destroy(currentBombObj);
            currentBombs--;
            bombCount++;
        }
    }

    public void detonateBomb(){
        if(currentBombObj == null || currentBombObj.GetComponent<Bomb>().chargeLevel == 0) return;
        bombSource.pitch = UnityEngine.Random.Range(0.8f,1.2f);
        bombSource.Play();
        currentBombObj.GetComponent<Bomb>().Detonate();
        currentBombs--;
    }
    
}
