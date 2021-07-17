using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int countDownTime = 30;
    private int timeLeft;
    public TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeText.enabled = false;
    }

    // Update is called once per frame
    public IEnumerator Timer(){
        if(GameManager.Instance.isDriving){
            timeText.enabled = true;
            timeLeft = countDownTime;
            timeText.text = "Time: " + timeLeft;
        }
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            timeText.text = "Time: " + timeLeft;
            if(!GameManager.Instance.isDriving){
                timeText.enabled = false;
                yield break;
            }
        }
        
        GameOver();
    }

    public void GameOver(){
        timeText.enabled = false;
        GameManager.Instance.stopPlaying();
        Debug.Log("Game Over!");
    }
}
