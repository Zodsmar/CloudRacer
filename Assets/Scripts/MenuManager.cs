using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    public GameObject helpMenu;
    // Start is called before the first frame update
    void Start(){
        // ES3AutoSaveMgr.Current.settings.path = Application.dataPath +"/Levels/Level Two.es3";
        // ES3AutoSaveMgr.Current.Load();
    }

    public void loadFreeplay(){
        SceneManager.LoadScene("RoadBuilder");
        LevelToLoad.isLevel = false;
    }
    
    public void QuitGame(){
         // save any game data here
     #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
     #else
         Application.Quit();
     #endif
    }

    public void OpenFolder() {
        #if UNITY_EDITOR
        EditorUtility.RevealInFinder(Application.persistentDataPath);
        #else
        Application.OpenURL(Application.persistentDataPath);
        #endif
    }

    public void ShowHelp(){
        if(helpMenu.activeInHierarchy){
            helpMenu.SetActive(false);
        } else {
            helpMenu.SetActive(true);
        }
    }
}
