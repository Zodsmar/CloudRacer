using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public GameObject vehicle;
    public bool isDriving = false;

    //public bool isPlaying = false;
    private GameObject instatiatedVehicle;
    
    private LevelManager levelManager;

    private Vector3 vehicleHeight = new Vector3(0, 0.2f, 0);

    public GameObject mCamera;

    public Vector3 prevCameraPos;
    public Quaternion prevCameraRot;

    public GameObject savePanel;
    public GameObject obstacle;

    public GameObject winPanel;

    public GameObject pausePanel;


    public SaveLevel saveUI;

    private bool didWin = false;
    
    public AudioClip winSound;

    private AudioSource playerSound;
    

    void Start() {
        prevCameraPos = mCamera.transform.position;
        prevCameraRot = mCamera.transform.rotation;
        levelManager = GetComponent<LevelManager>();
        
        LevelToLoad.loadLevel();
        if(LevelToLoad.isLevel){
            obstacle.SetActive(false);
        }
        playerSound = GetComponent<AudioSource>();
        
    }

    private void Update() {
        if(!isDriving){
            if(Input.GetKeyDown(KeyCode.Escape)){
            //SceneManager.LoadScene("MainMenu");
            //Pause Menu
            GameManager.Instance.pauseGame();
            }
        }
        if(didWin){
             if(Input.GetKeyDown(KeyCode.Escape)){
                 winPanel.SetActive(false);
                 didWin = false;
             }
        }
    }
    public void startPlaying() {
        if(!GameObject.FindGameObjectWithTag("Start")) return;
        if(isDriving) return;
        isDriving = true;
        Transform start = GameObject.FindGameObjectWithTag("Start").transform;
        instatiatedVehicle = Instantiate(vehicle, start.position + vehicleHeight, start.transform.rotation * vehicle.transform.rotation);
        mCamera.GetComponent<CameraController>().changeAudio();
        levelManager.countDownTime = saveUI.setTime();
        StartCoroutine(levelManager.Timer());
    }

    public void ResetVehicle(){
        Destroy(instatiatedVehicle);
        Transform start = GameObject.FindGameObjectWithTag("Start").transform;
        instatiatedVehicle = Instantiate(vehicle, start.position + vehicleHeight, start.transform.rotation * vehicle.transform.rotation);
        instatiatedVehicle.GetComponent<CarController>().playCrash();
    }

    public void stopPlaying() {
        isDriving = false;
        Destroy(instatiatedVehicle);
        mCamera.transform.position = prevCameraPos;
        mCamera.transform.rotation = prevCameraRot;
        mCamera.GetComponent<CameraController>().changeAudio();
    }

    public GameObject getVehicle() {
        return instatiatedVehicle;
    }

    public void makeLevel(){

        
        savePanel.SetActive(true);
        //ES3AutoSaveMgr.Current.Save();
        //ES3.Save("MyScene", SceneManager.GetActiveScene().name);

    }

    public void SetDestroyable(){
        List<GameObject> gameObjectsToSet = new List<GameObject> (GameObject.FindGameObjectsWithTag("Start"));
        gameObjectsToSet.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Checkpoint")));
        gameObjectsToSet.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle")));
        gameObjectsToSet.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Road")));

        foreach(GameObject obj in gameObjectsToSet){
            obj.GetComponent<Destroyable>().setDestroyable(false);
        }
    }

    public void saveLevel(int time, string levelName, bool destroy){
        if(!destroy) {
            SetDestroyable();
        }

        savePanel.SetActive(false);
        ES3AutoSaveMgr.Current.settings.path = levelName;

        levelManager.countDownTime = time;
        ES3AutoSaveMgr.Current.Save();
    }


    public void win() {
        winPanel.SetActive(true);
        didWin = true;
        playerSound.PlayOneShot(winSound);
    }

    public void pauseGame(){
        if(didWin) return;
        if(pausePanel.activeInHierarchy){
            pausePanel.SetActive(false);
        } else {
            pausePanel.SetActive(true);
        }
    }

    public void loadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

}
