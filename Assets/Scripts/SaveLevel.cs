using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveLevel : MonoBehaviour
{
    public TMP_InputField timeField;
    public TMP_InputField nameField;

    public Toggle toggle;
    public void save(){
       int number = setTime();
        if(nameField.text == ""){
            nameField.text = "default";
        }
        
        GameManager.Instance.saveLevel(number, nameField.text + ".es3", toggle.isOn);
   }

   public void cancel(){
       gameObject.SetActive(false);
   }

   public int setTime(){
       int number;
        if(!int.TryParse(timeField.text, out number)){
            number =  30;
        }
        return number;
    }
}
