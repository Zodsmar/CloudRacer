using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private bool isDestroyable = true;

    public void setDestroyable(bool dest){
        isDestroyable = dest;
    }

     public bool getDestroyable(){
        return isDestroyable;
    }
}
