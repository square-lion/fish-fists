using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour {


	public Text names;
	public Text scores;


	Highscores highscoreManager;

	public InputField username;
	public Text feedbackText;
	public Text nameText;

	//Podium
	public Text name1;
	public Text score1;
	public Text name2;
	public Text score2;
	public Text name3;
	public Text score3;

	public List<string> usernames;
	// Use this for initialization
	void Start () {		 
		scores.text = "Fetching...";			
		highscoreManager = GetComponent<Highscores>();
		StartCoroutine("RefreshHighscores");

		if(PlayerPrefs.GetString("Username") != ""){
			if(username != null)
				username.gameObject.SetActive(false);
			Debug.Log("Sending Score");
			highscoreManager.AddNewHighscore(PlayerPrefs.GetString("Username"), PlayerPrefs.GetInt("Highscore"));
			nameText.text = "Username: " + PlayerPrefs.GetString("Username");
		}
		else{
			if(username != null){
				username.gameObject.SetActive(true);
			}else{
				username.gameObject.SetActive(false);
			}
		}
	}

	public void OnHighscoresDownloaded(Highscore[] highscoreList){

		names.text = "";
		scores.text = "";
		bool displayed = false;
		for(int i = 0; i < highscoreList.Length; i++)
		{
			if(i == 0){
				name1.text = "" + highscoreList[i].username;
				score1.text = "" + highscoreList[i].score;
			}
			else if(i == 1){
				name2.text = "" + highscoreList[i].username;
				score2.text = ""  + highscoreList[i].score;
			}
			else if(i == 2){
				name3.text = "" + highscoreList[i].username;
				score3.text = "" + highscoreList[i].score;
			}

			if(i < 10){
				names.text += highscoreList[i].username + "\n";
				scores.text += highscoreList[i].score + "\n";
			}
			else if(!displayed){
				if(highscoreList[i].username.Equals(PlayerPrefs.GetString("Username"))){
					names.text += highscoreList[i].username + "\n";
					scores.text += highscoreList[i].score + "\n";
				}
			}

			if(highscoreList[i].username.Equals(PlayerPrefs.GetString("Username"))){
				displayed = true;
			}

			usernames.Add(highscoreList[i].username.ToString());
		}
	}

	public void OnHighscoresFailed(){
		scores.text = "No Connection";
	}

	IEnumerator RefreshHighscores()
	{
		while(true)
		{
			highscoreManager.DownloadHighscores();
			yield return new WaitForSeconds(10f); 
		}
	}

	public void Submit(){
		print(username.text);

		if(username.text.ToString() == "")
			feedbackText.text = "Enter username";
		else if(usernames.IndexOf(username.text.ToString().ToLower()) != -1)
			feedbackText.text = "Username taken";
		else if(username.text.ToString().IndexOf(" ") != -1){
			feedbackText.text = "Spaces not allowed";
		}
		else{
			print(highscoreManager);
			FindObjectOfType<Highscores>().AddNewHighscore(username.text.ToLower(), PlayerPrefs.GetInt("Highscore"));
			PlayerPrefs.SetString("Username", username.text.ToLower());
			Debug.Log(username.text);
			nameText.text = "Username: " + PlayerPrefs.GetString("Username");
			username.gameObject.SetActive(false);
		}	
	}
}