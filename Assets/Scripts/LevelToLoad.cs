using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelToLoad
{
   public static bool isThereAFileToLoad { get; set; }
   public static string fileToLoad { get; set; }

   public static bool isLevel { get; set; }

   public static void loadLevel(){
       if(LevelToLoad.isThereAFileToLoad){
            //ES3AutoSaveMgr.Current.settings.path = "Test.es3";
            //ES3AutoSaveMgr.Current.settings.path = fileToLoad;
            if(isLevel){
                ES3AutoSaveMgr.Current.settings.path = Application.streamingAssetsPath +"/Levels/"+ fileToLoad;
            } else {
                ES3AutoSaveMgr.Current.settings.path = fileToLoad;
            }
            ES3AutoSaveMgr.Current.Load();
       }
       isThereAFileToLoad = false;
       fileToLoad = "default.es3";
   }
}
