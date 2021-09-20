using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject book;
    public GameObject settings;
    public GameObject leaderboard;

    public Slider musicSlider;
    public Slider sfxSlider;

    public Text highscore;

    AudioManager audioManager;

    void Awake(){
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.Play("Menu");

        musicSlider.value = PlayerPrefs.GetFloat("Menu");
        sfxSlider.value = PlayerPrefs.GetFloat("Click");

        highscore.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }
      
    public void Play(){
        //FindObjectOfType<WatchAd>().ShowAd();
        SceneManager.LoadScene("Main");
        audioManager.Play("Click");
    }

    public void OnMusicChange(Slider s){
        PlayerPrefs.SetFloat("Menu", s.value);
        PlayerPrefs.SetFloat("Music", s.value);
        PlayerPrefs.SetFloat("End", s.value);

        audioManager.ChangeVolume("Menu", s.value);
    }

    public void OnSFXChange(Slider s){
        PlayerPrefs.SetFloat("Click", s.value);
        PlayerPrefs.SetFloat("Punch", s.value);

        audioManager.ChangeVolume("Click", s.value);
    }

    //Settings
    public void OpenSettings(){
        settings.SetActive(true);
        audioManager.Play("Click");
    }
    public void CloseSettings(){
        settings.SetActive(false);
        audioManager.Play("Click");
    }

    //Marine
    public void OpenBook(){
        book.SetActive(true);
        audioManager.Play("Click");
    }
    public void CloseBook(){
        book.SetActive(false);
        audioManager.Play("Click");
    }

    //Leaderboard
    public void OpenLeaderboard(){
        leaderboard.SetActive(true);
        audioManager.Play("Click");
    }
    public void CloseLeaderboard(){
        leaderboard.SetActive(false);
        audioManager.Play("Click");
    }
}
