using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



public class SoundSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider projSlider;
    [SerializeField] Slider explosionSlider;

    string masterVolume = "MasterVolume";
    string musicVolume = "MusicVolume";
    string sfxVolume = "SFXVolume";
    string projectileVolume = "ProjectileVolume";
    string explosionVolume = "ExplosionVolume";

    void Start(){
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolume, 0f);
        musicSlider.value = PlayerPrefs.GetFloat(musicVolume, 0f);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxVolume, 0f);
        projSlider.value = PlayerPrefs.GetFloat(projectileVolume, 0f);
        explosionSlider.value = PlayerPrefs.GetFloat(explosionVolume, 0f);
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetProjectileVolume();
        SetExplosionVolume();
    }
    public void SetMasterVolume(){
        SetVolume(masterVolume, masterVolumeSlider.value);
        PlayerPrefs.SetFloat(masterVolume, masterVolumeSlider.value);
    }
    public void SetMusicVolume(){
        SetVolume(musicVolume, musicSlider.value);
        PlayerPrefs.SetFloat(musicVolume, musicSlider.value);
    }
    public void SetSFXVolume(){
        SetVolume(sfxVolume, sfxSlider.value);
        PlayerPrefs.SetFloat(sfxVolume, sfxSlider.value);
    }
    public void SetProjectileVolume(){
        SetVolume(projectileVolume, projSlider.value);
        PlayerPrefs.SetFloat(projectileVolume, projSlider.value);
    }
    public void SetExplosionVolume(){
        SetVolume(explosionVolume, explosionSlider.value);
        PlayerPrefs.SetFloat(explosionVolume, explosionSlider.value);
    }


    void SetVolume(string groupName, float value){
        float adjustedVol = Mathf.Log10(value) * 20;
        if(value == 0) adjustedVol = -80;
        audioMixer.SetFloat(groupName, adjustedVol);

    }
}
