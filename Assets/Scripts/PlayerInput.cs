using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
public class PlayerInput : MonoBehaviour
{
    [SerializeField] Player playerObject;
    [SerializeField] PauseMenu pauseObject;
    [SerializeField] PlayerProjectileSpawner spawnerObject;
    [SerializeField] public int upSpeed;
    [SerializeField] public float rangeCooldownTime;
    private int throwspeed = 0;
    private bool rangeReady = true;

    [SerializeField] private InputActionReference left, right, up, down, shoot, swap, place, retrieve, detonate, jump, throwBomb, pauseMenu;

    
    private void OnEnable(){
        shoot.action.performed += ShootProjectile;
        if(playerObject.isGrounded) jump.action.performed += Jump;
        up.action.started += UpPressed;
        up.action.canceled += UpReleased;
        swap.action.performed += SwapPolarity;
        place.action.performed += PlaceBomb;
        retrieve.action.performed += RetrieveBomb;
        detonate.action.performed += DetonateBomb;
        pauseMenu.action.performed += PauseGame;
    }
    private void OnDisable(){
        shoot.action.performed -= ShootProjectile;
        if(playerObject.isGrounded) jump.action.performed -= Jump;
        up.action.started -= UpPressed;
        up.action.canceled -= UpReleased;
        swap.action.performed -= SwapPolarity;
        place.action.performed -= PlaceBomb;
        retrieve.action.performed -= RetrieveBomb;
        detonate.action.performed -= DetonateBomb;
        pauseMenu.action.performed -= PauseGame;
    }
   
    private void ShootProjectile(InputAction.CallbackContext obj){
        if(rangeReady){
            spawnerObject.SpawnRanged();
            StartCoroutine(rangeCooldown());
        }
    }
    private void Jump(InputAction.CallbackContext obj){
        playerObject.Jump();
    }
    private void SwapPolarity(InputAction.CallbackContext obj){
        playerObject.changePolarity();
    }
    private void PlaceBomb(InputAction.CallbackContext obj){ 
        spawnerObject.placeBomb(throwspeed);
    }
    private void UpPressed(InputAction.CallbackContext obj){
        throwspeed = upSpeed;  
    }
    private void UpReleased(InputAction.CallbackContext obj){
        throwspeed = 0;  
    }
    private void RetrieveBomb(InputAction.CallbackContext obj){
        playerObject.retrieveBomb();
    }
    private void DetonateBomb(InputAction.CallbackContext obj){
        playerObject.detonateBomb();
    }
    private void PauseGame(InputAction.CallbackContext obj){
        pauseObject.Pause();
    }
    

    // Update is called once per frame
    void Update()
    {
        /*//Swap polarity
        if(Input.GetKeyDown(KeyCode.U)){
            playerObject.changePolarity();
        }
        //Jump
        if(Input.GetKeyDown(KeyCode.Space) && playerObject.isGrounded){
            //playerObject.Jump();
        }
        //Shoot Ranged Projectile
        if(Input.GetKeyDown(KeyCode.I)){
            if(rangeReady){
                //spawnerObject.SpawnRanged();
                //StartCoroutine(rangeCooldown());
            }
        }
        //Melee Swing


        //Bomb
        if(Input.GetKeyDown(KeyCode.E)){
            int throwspeed = 0;
            if(Input.GetKey(KeyCode.W)){
                throwspeed = upSpeed;
            }
            spawnerObject.placeBomb(throwspeed);
        }
        if(Input.GetKeyDown(KeyCode.R)){
            playerObject.retrieveBomb();
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            playerObject.detonateBomb();
        }
        */
    }

    void FixedUpdate(){
       
        Vector3 moveVector = Vector3.zero;
        if(left.action.inProgress){
            moveVector = new Vector3(-1,0,0); //5
            playerObject.flipSprite(true);
            playerObject.Move(moveVector);
        }
        if(right.action.inProgress){
            moveVector = new Vector3(1,0,0); //5
            playerObject.flipSprite(false);
            playerObject.Move(moveVector);
        }
    }

    public Player getPlayer(){
        return playerObject;
    }

    IEnumerator rangeCooldown(){
        rangeReady = false;
        yield return new WaitForSeconds(rangeCooldownTime);
        rangeReady = true;
    }
    
}


