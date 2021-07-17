using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayLevels : MonoBehaviour
{
    public ScrollRect scrollViewLevel;
    public GameObject contentPanelLevel;
    public GameObject levelPanel;

    public ScrollRect scrollViewSaves;
    public GameObject contentPanelSaves;
    public GameObject savesPanel;

    public GameObject levelPrefab;

    

    void generateViews(string levelName, bool isLevel, GameObject contentPanel, GameObject viewPanel){

        GameObject scrollObj = Instantiate(levelPrefab);
        scrollObj.transform.SetParent(contentPanel.transform, false);
        scrollObj.GetComponentInChildren<Button>().onClick.AddListener(delegate{loadLevel(levelName, isLevel);});
        scrollObj.GetComponentInChildren<Button>().onClick.AddListener(delegate{viewPanel.SetActive(false);});
        
        
        int levelExtPos = levelName.LastIndexOf(".");
        scrollObj.GetComponentInChildren<TextMeshProUGUI>().text = levelName.Substring(0, levelExtPos);;
        
    }

    public void showLevels(){
        destroyPanelChildren(contentPanelLevel);
        string[] levels = ES3.GetFiles(Application.streamingAssetsPath +"/Levels/");
        for(int i = 0; i < levels.Length; i++){
            if(!levels[i].Contains(".meta")){
                generateViews(levels[i], true, contentPanelLevel, levelPanel);
            }
        }
        scrollViewLevel.verticalNormalizedPosition = 1;
        if(levelPanel.activeInHierarchy){
            levelPanel.SetActive(false);
        } else {
            levelPanel.SetActive(true);
        }
        
    }


    public void showSavedGames(){
        destroyPanelChildren(contentPanelSaves);
        string[] levels = ES3.GetFiles(Application.persistentDataPath);
        for(int i = 0; i < levels.Length; i++){
            if(levels[i].Contains(".es3")){
                generateViews(levels[i], false, contentPanelSaves, savesPanel);
            }
        }
        scrollViewSaves.verticalNormalizedPosition = 1;
        if(savesPanel.activeInHierarchy){
            savesPanel.SetActive(false);
        } else {
            savesPanel.SetActive(true);
        }
        
    }
    public void destroyPanelChildren(GameObject panel){

         foreach (Transform child in panel.transform) {
            Destroy(child.gameObject);
        }
    }

    public void loadLevel(string level, bool isLevel){
        LevelToLoad.isThereAFileToLoad = true;
        LevelToLoad.fileToLoad = level;
        LevelToLoad.isLevel = isLevel;
        SceneManager.LoadScene("RoadBuilder");
       
    }
}
