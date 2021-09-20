using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject main;
    public GameObject gameOver;

    [Header("Score Variables")]
    public int score;
    public Text scoreText;
    public int fromFish;
    public int fromCombo;
    public Text depthText;
    public Text depthToGoText;

    [Header("Touch Controls")]
    public GameObject joystick;
    public GameObject punchButton;

    [Header("Camera Variables")]
    public Camera c;
    public float cameraMoveSpeed;
    Vector3 cameraPosition;

    [Header("Spawner Variables")]
    public float depth;
    public float spawnDelay;
    public GameObject[] fish;
    public GameObject[] spawners;
    public float[] spawnGroupEnds;

    [Header("Combo Variables")]
    public Text comboText;
    public int combo;
    public Slider comboSlider;
    public float comboTime;
    public float curComboTime;

    [Header("Game Over Variables")]
    public Text endScoreText;
    public Text fromPunchText;
    public Text fromComboText;
    public Text highscoreText;

    AudioManager audioManager;

    void Awake(){
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start(){
        cameraPosition = c.transform.position;

        InvokeRepeating("SpawnFish", spawnDelay, spawnDelay);

        comboSlider.maxValue = comboTime;

        audioManager.Play("Music");
    }

    void Update(){
        if(cameraPosition.y < -245)
            AlmostAtBottom();

        if(cameraPosition.y > -253){
        cameraPosition.y -= cameraMoveSpeed * Time.deltaTime;
        c.transform.position = cameraPosition;
        depth = cameraPosition.y;
        }
        else{
            AtBottom();
        }


        if(curComboTime > 0){
            curComboTime -= Time.deltaTime;
            comboSlider.value = curComboTime;
        }
        else if(combo > 0)
            ComboPunch(0);

        depthText.text = "\n" + (int)(depth/-3) + "m";
        depthToGoText.text = (253/3)+(int)(depth/3) + "m to go";
    }  


    
    public void AddPoints(int points){
        fromFish += points;
        score = fromFish + fromCombo;
        scoreText.text = "Score: " + score;
    }

    public void SpawnFish(){
        float d = -depth/3f;

        if(d < 7)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(0,3)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 14)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(1,4)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 21)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(2,5)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 28)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(3,6)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 35)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(4,7)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 42)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(5,8)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 49)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(6,9)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 56)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(7,10)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 63)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(8,11)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 70)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(9,12)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 77)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(10,12)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        else if(d < 80)
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(11,12)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);

        /*
        if(depth > spawnGroupEnds[0]){
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(0,3)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        }
        else if(depth > spawnGroupEnds[1]){
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(3,6)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        }
        else if(depth > spawnGroupEnds[2]){
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(6,9)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        }
        else if(depth > spawnGroupEnds[3]){
            foreach(GameObject s in spawners)
                Instantiate(fish[Random.Range(9,12)], new Vector3(s.transform.position.x, Random.Range(s.transform.position.y-20, s.transform.position.y), s.transform.position.z), s.transform.rotation);
        }
        */
    }

    public void ComboPunch(int hit){
        if(hit != 0){
            combo += hit;
            comboText.text = combo + "x Combo";
            comboSlider.gameObject.SetActive(true);

            curComboTime = comboTime;
        }
        else{
            if(combo > 0){
                fromCombo += combo;
                AddPoints(0);
            }

            combo = 0;
            comboText.text = "";
            comboSlider.gameObject.SetActive(false);
        }
    }

    public void AlmostAtBottom(){
        FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<PlayerController>().GetComponent<CapsuleCollider2D>().enabled = false;

        punchButton.SetActive(false);
        joystick.SetActive(false);
    }

    public void AtBottom(){
        c.GetComponent<Animator>().enabled = true;
        
        main.SetActive(false);
        Invoke("GameOver", 20);
    }

    public void GameOver(){
        gameOver.SetActive(true);

        audioManager.Stop("Music");
        audioManager.Play("End");

        endScoreText.text = "Score: " + score;
        fromPunchText.text = "Points From Punches: " + fromFish;
        fromComboText.text = "Points From Combos: " + fromCombo;

        if(score > PlayerPrefs.GetInt("Highscore")){
            PlayerPrefs.SetInt("Highscore", score);
        }
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore");
    }

    public void Menu(){
        FindObjectOfType<WatchAd>().ShowAd();
        audioManager.Play("Click");
        SceneManager.LoadScene("Menu");
    }

    public void Restart(){
        FindObjectOfType<WatchAd>().ShowAd();
        audioManager.Play("Click");
        SceneManager.LoadScene("Main");
    }
}


