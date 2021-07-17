using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointChecker : MonoBehaviour
{
    public bool passed = false;
    // Start is called before the first frame update

    public void setPassed(bool isPassed = true){
        passed = isPassed;
    }

    public bool getPassed(){
        return passed;
    }
}
