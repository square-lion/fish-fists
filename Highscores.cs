using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

public class Highscores : MonoBehaviour {
   
    const string privateCode = "pBGEqfvh_0C-JG1Ol9Cl0AMvkxfLm94EyKUwCBqOd6IA";
    const string publicCode = "6111c1f48f40bb25a0f0f411";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;
    DisplayHighscores display;

    void Awake(){
        display = GetComponent<DisplayHighscores>();

        //DownloadHighscores();
    }

    public void AddNewHighscore(string username, int score){
        StartCoroutine(UploadNewHighscore(username, score));
    }

    IEnumerator UploadNewHighscore(string username, float score){
        UnityWebRequest www = UnityWebRequest.Get(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        www.SendWebRequest(); 
        yield return www;

        if(string.IsNullOrEmpty(www.error))
            print("Upload Successful");
        else
            print("Error Uploading" + www.error);
   }

   public void DownloadHighscores(){
       StartCoroutine("DownloadHighscoresFromDatabase");
   }

   IEnumerator DownloadHighscoresFromDatabase(){
        UnityWebRequest www = UnityWebRequest.Get(webURL + publicCode + "/pipe/");
        yield return www.SendWebRequest();

        if(string.IsNullOrEmpty(www.error)){
            FormatHighscores(www.downloadHandler.text);
            display.OnHighscoresDownloaded(highscoresList);
        }
        else{
            print("Error Downloading" + www.error);
            display.OnHighscoresFailed();
        }
   }

   void FormatHighscores(string textStream){
       string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
       highscoresList = new Highscore[entries.Length];

       for(int i = 0; i < entries.Length; i ++){
           string[] entryInfo = entries[i].Split(new char[] {'|'});
           string username = entryInfo[0];
           int score = int.Parse(entryInfo[1]);
           highscoresList[i] = new Highscore(username, score);
           //print(highscoresList[i].username + ": " + highscoresList[i].time);
       }
   }
}

public struct Highscore { 
    public string username;
    public float score;

    public Highscore(string _username, int _score){
        username = _username;
        score = _score;
    }
}
