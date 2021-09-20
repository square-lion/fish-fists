using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
	public Audio[] audios;

	void Awake(){
		foreach(Audio a in audios){
			a.source = gameObject.AddComponent<AudioSource>();
			a.source.clip = a.clip;
			a.source.volume = PlayerPrefs.GetFloat(a.name);
			a.source.pitch = a.pitch;
			a.source.loop = a.loop;
		}

		//If First Time Opening Game
        if(PlayerPrefs.GetInt("NewGame") == 0){
            PlayerPrefs.SetFloat("Menu", 1);
            PlayerPrefs.SetFloat("Music", 1);
            PlayerPrefs.SetFloat("End", 1);
            PlayerPrefs.SetFloat("Punch", 1);
            PlayerPrefs.SetFloat("Click", 1);
            PlayerPrefs.SetInt("NewGame", 1);

			foreach(Audio a in audios){
				a.source.volume = 1;
			}
        }


        //PlayerPrefs.SetInt("NewGame", 0);

		//Play("Menu");
	}

	public void Play(string name){
		Audio a = Array.Find(audios, audio => audio.name == name);

		if(a == null)
			Debug.LogError("Could not find audio with name " + name);
		else{
			if(!a.source.isPlaying)
				a.source.Play();
		}
	}	

	 public void Stop(string name){
		Audio a = Array.Find(audios, audio => audio.name == name);
		a.source.Stop();
	}	

	public void ChangeVolume(string name, float value){
		Audio a = Array.Find(audios, audio => audio.name == name);

		if(a == null)
			Debug.LogError("Could not find audio with name " + name);
		else{
			a.source.volume = value;
		}
	}
}

