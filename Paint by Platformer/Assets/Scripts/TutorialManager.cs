using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popups;
    private int popupIndex;

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<popups.Length;i++){
            if(i==popupIndex){
                popups[i].SetActive(true);
            }else{
                popups[i].SetActive(false);
            }
        }

        //tutorial popups
        if(popupIndex==0){ //left right popup
            Debug.Log(Gamepad.current.leftStick.ReadValue().x);
            if(Gamepad.current.leftStick.ReadValue().x>0.8 ||
            Gamepad.current.leftStick.ReadValue().x<-0.8){
                popupIndex++;
            }
        } else if(popupIndex==1){ //jump popup
            if(Gamepad.current.aButton.wasPressedThisFrame){
                popupIndex++;
            }
        }
    }
}
