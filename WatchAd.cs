using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Advertisements;
using UnityEngine.XR;

public class WatchAd : MonoBehaviour{
    void Awake(){
        #if UNITY_IPHONE
            Advertisement.Initialize ("4255416", false);
        #endif
        #if UNITY_ANDROID
            Advertisement.Initialize ("4255417", false);
        #endif
    }

    public void ShowAd(){
        
        if (Advertisement.IsReady() && PlayerPrefs.GetInt("PlayAds") == 0) {
            Debug.Log("Play Ad");
            Advertisement.Show();
        }      
    } 
}
