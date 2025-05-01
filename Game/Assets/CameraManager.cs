using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera gameCam;
    public Camera scriptCam;
    public CharacterControllerScript scriptChar;
    public PlayerMovemetnPhysics gameChar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchToGameCam(){
        scriptChar.canMove = false;
        gameChar.onCam = true;
        gameCam.gameObject.SetActive(true);
        scriptCam.gameObject.SetActive(false);
    }
    public void switchToScriptCam(){
        scriptChar.canMove = true;
        gameChar.onCam = false;
        gameCam.gameObject.SetActive(false);
        scriptCam.gameObject.SetActive(true);
    }
}
