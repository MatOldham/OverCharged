using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class playerInfoUI : MonoBehaviour
{
    [SerializeField] Player playerObj;
    [SerializeField] TextMeshProUGUI chargeText, bombText, sceneText;
    [SerializeField] string Prefix;


    void Awake(){
        sceneText.text = SceneManager.GetActiveScene().name;
    }
    // Update is called once per frame
    void Update()
    {
        chargeText.text = Prefix + playerObj.chargeLevel;
        bombText.text = Prefix + playerObj.bombCount;
        
    }
}
