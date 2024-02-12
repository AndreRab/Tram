using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class statics : MonoBehaviour
{
    public static float speed = 0.01f;

    public static int score = 0;
    public static int bestScore;
    public static int coins;
    public static int colorKoef = 1;

    public static bool canSpawn = false;
    public static bool endGame = false;
    public static bool canTouch = true;
    public static bool inPlay = false;
    public static bool canAnimTram = false;
    public static bool canPlaySound = true;
    public static bool canSpawnPeople = true;
    public static bool stopSpawn = false;
    public static bool canInAd = false;
    private bool newScore = false;
    private bool wasPlayDied = false;

    public static string activeColor = "RED";

    public GameObject[] Trafs;
    public GameObject[] SoundOff;
    public GameObject[] SoundOn;

    public GameObject endCanvas;
    public GameObject StartCanvas;
    public GameObject PlayCanvas;
    public GameObject pauseCanvas;
    public GameObject shopCanvas;
    public GameObject shopTrams;
    public GameObject Tram;
    public GameObject blueBuy;
    public GameObject blueSelect;
    public GameObject greenBuy;
    public GameObject greenSelect;
    public GameObject tramMat;

    public Material redMat;
    public Material blueMat;
    public Material greenMat;
    
    public Animator anim;
    public Animator animEnd;
    public Animator animatorStart;
    public Animator animatorStore;
    public Animator animatorStoreTram;

    public AudioSource endSound;

    public Text txt_score;
    public Text txt_coins_in_play;
    public Text txt_score_end;
    public Text txt_first_best_score;
    public Text txt_coins;

    public InterstitialAds ad;

    void Start(){
        Application.targetFrameRate = 60;
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        coins = PlayerPrefs.GetInt("Coins", 0);
        colorKoef = PlayerPrefs.GetInt("Colors", 1);
        activeColor = PlayerPrefs.GetString("ActiveColor", "RED");
        updateTramColor();
        if (colorKoef % 2 == 0){
            blueBuy.SetActive(false);
            blueSelect.SetActive(true);
        }
        if (colorKoef % 3 == 0){
            greenBuy.SetActive(false);
            greenSelect.SetActive(true);
        }
        txt_first_best_score.text = "BEST SCORE: " +statics.bestScore;
    }

    void Update(){
        txt_score.text = "SCORE: "+statics.score;
        txt_coins.text = "ITEMS: " + statics.coins;
        txt_coins_in_play.text = "ITEMS: " +statics.coins;
        if(newScore){
            txt_score_end.text = "NEW BEST SCORE: "+statics.score;
        }
        else{
            txt_score_end.text = "YOUR SCORE: "+statics.score;
        }

        if(endGame){
            endGame = false;
            stopSpawn = true;
            endCanvas.SetActive(true);
            if (canPlaySound && wasPlayDied == false){
                wasPlayDied = true;
                endSound.Play();
            }
            PlayCanvas.SetActive(false);
            if(score > bestScore){
                newScore = true;
                bestScore = score;
                PlayerPrefs.SetInt("BestScore", score);
            }
            clearTraf();
        }

        if(inPlay && canAnimTram){
            //Tram.SetActive(true);
            anim.SetBool("PlayAnim", true);
            canAnimTram = false;
        }
    }

    public void newGame(){
        statics.score = 0;
        canAnimTram = true;
        endCanvas.SetActive(false);
        PlayCanvas.SetActive(true);
        canTouch = true;
        canSpawn = true;
        canSpawnPeople = true;
        newScore = false;
        stopSpawn = false;
        wasPlayDied = false;
        animatorStoreTram.enabled = false;
        ad.ShowAd();
    }

    public void StartGame(){
        statics.score = 0;
        inPlay = true;
        canAnimTram = true;
        //StartCanvas.SetActive(false);
        PlayCanvas.SetActive(true);
        canSpawn = true;
        stopSpawn = false;
        animatorStart.SetBool("canClose", true);
        animatorStart.SetBool("canCloseAndPlay", true);
        StartCoroutine(Wait2Sec());
    }

    public void clearTraf(){
        Trafs = GameObject.FindGameObjectsWithTag("TrafController");
        foreach (GameObject traf in Trafs){
            Destroy(traf);
        }
    }

    public void pause(){
        pauseCanvas.SetActive(true);
        
        PlayCanvas.SetActive(false);
        Time.timeScale = 0; 
        stopSpawn = true;
    }

    public void countinue(){
        pauseCanvas.SetActive(false);
        PlayCanvas.SetActive(true);
        Time.timeScale = 1; 
        stopSpawn = false;
    }

    public void openShop(){
        //Tram.SetActive(false);
        if(inPlay){
            animatorStoreTram.enabled = true;
            animEnd.SetBool("canClose", true);
            animEnd.SetBool("closeShop", false);
            animatorStoreTram.SetBool("tramDis", true);
        } 
        else{
            animatorStart.SetBool("canClose", true);
        }
        shopCanvas.SetActive(true);
        shopTrams.SetActive(true);
        
    }

    public void closeShop(){
        //Tram.SetActive(true);
        shopCanvas.SetActive(false);
        //shopTrams.SetActive(false);
        animatorStore.SetBool("closeStore", true);
        if(inPlay){
            animEnd.SetBool("closeShop", true);
            animEnd.SetBool("canClose", false);
            animatorStoreTram.SetBool("tramDis", false);
        } 
        else{
            animatorStart.SetBool("canClose", false);
            animatorStart.SetBool("closeShop", true);
        }
        StartCoroutine(Wait2SecShop());
    }

    public void SoundOnFn(){
        foreach (GameObject button in SoundOn){
            button.SetActive(false);
        }

        foreach (GameObject button in SoundOff){
            button.SetActive(true);
        }

        canPlaySound = true;
    }

    public void SoundOffFn(){
        foreach (GameObject button in SoundOn){
            button.SetActive(true);
        }

        foreach (GameObject button in SoundOff){
            button.SetActive(false);
        }

        canPlaySound = false;
    }

    public void redButton(){
        activeColor = "RED";
        PlayerPrefs.SetString("ActiveColor", "RED");
        updateTramColor();
    }

    public void blueButton(){
        if(coins >= 100 && colorKoef % 2 != 0){
            coins = coins - 100;
            PlayerPrefs.SetInt("Coins", coins);
            colorKoef = colorKoef * 2;
            PlayerPrefs.SetInt("Colors", colorKoef);
            blueBuy.SetActive(false);
            blueSelect.SetActive(true);
        }

        if(colorKoef % 2 == 0){
            activeColor = "BLUE";
            PlayerPrefs.SetString("ActiveColor", "BLUE");
            updateTramColor();
        }
    }

    public void greenButton(){
        if(coins >= 100 && colorKoef % 3 != 0){
            coins = coins - 100;
            PlayerPrefs.SetInt("Coins", coins);
            colorKoef = colorKoef * 3;
            PlayerPrefs.SetInt("Colors", colorKoef);
            greenBuy.SetActive(false);
            greenSelect.SetActive(true);
        }

        if(colorKoef % 3 == 0){
            activeColor = "GREEN";
            PlayerPrefs.SetString("ActiveColor", "GREEN");
            updateTramColor();
        }
    }

    void updateTramColor(){
        if (activeColor == "RED"){
            tramMat.GetComponent<Renderer>().material = redMat;
        }
        else if (activeColor == "BLUE"){
            tramMat.GetComponent<Renderer>().material = blueMat;
        }
        else if(activeColor == "GREEN"){
            tramMat.GetComponent<Renderer>().material = greenMat;
        }
    }

    IEnumerator Wait2Sec(){
        yield return new WaitForSeconds(2f);
        StartCanvas.SetActive(false);
    }

    IEnumerator Wait2SecShop(){
        yield return new WaitForSeconds(2.5f);
        shopTrams.SetActive(false);
        animatorStart.SetBool("closeShop", false);
    }

    // IEnumerator Wait2SecEnd(){
    //     yield return new WaitForSeconds(3f);
    //     endCanvas.SetActive(false);
    //     animEnd.SetBool("closeShop", false);
    // }
}