using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScreenSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Toggle vsyncToggle;
    
    Resolution[] resolutions;
    
    void Start(){
        resolutions = Screen.resolutions;
        Resolution currentResolution = Screen.currentResolution;
        int currentResolutionIdx = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length-1);
        int fullscreen = PlayerPrefs.GetInt("Fullscreen", 1);
        int vsync = PlayerPrefs.GetInt("Vsync", 1);
        for(int i = 0; i < resolutions.Length; i++){
            string resolutionString = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString();
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionString));
        }
        currentResolutionIdx = Math.Min(currentResolutionIdx, resolutions.Length-1);
        resolutionDropdown.value = currentResolutionIdx;
        fullscreenToggle.isOn = (fullscreen != 0);
        vsyncToggle.isOn = (vsync != 0);


    }

    public void SetResolution(){
        int resIdx = resolutionDropdown.value;
        Screen.SetResolution(resolutions[resIdx].width, resolutions[resIdx].height, true);
        PlayerPrefs.SetInt("ResolutionIndex", resIdx);
    }
    public void SetFullscreen(){
        Screen.fullScreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt("Fullscreen", (fullscreenToggle.isOn ? 1 : 0));
    }
    public void SetVsync(){
        QualitySettings.vSyncCount = (vsyncToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Vsync", (vsyncToggle.isOn ? 1 : 0));
    }



}
