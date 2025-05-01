using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject startScreenBig;
    public GameObject computerScreen;
    public GameObject engine;
    public GameObject requestHelpPopup;
    public GameObject folderWindow;
    public GameObject LegWindow;
    public GameObject endScreen;
    public GameObject settingScreen;
    public GameObject startGameButton;
    public ErrorCodeHandler errorCodeHandler;
    public enum menuOptions{
        none,
        quit,
        play,
        settings
   
    }

    public menuOptions quitAction = menuOptions.none;
    public menuOptions settingsAction = menuOptions.none;
    public menuOptions playAction = menuOptions.none;
    public bool unlockedLeg = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void unlockLeg(){
        unlockedLeg = true;
        startGameButton.SetActive(false);

    }

    public void RequestHelp(){
        requestHelpPopup.SetActive(true);
    }

    public void OpenFolder(){
        folderWindow.SetActive(true);
    }

    public void OpenGamJam(){
        folderWindow.SetActive(true);
    }

    public void CloseWindow(){
        folderWindow.SetActive(false);
    }
    
    public void OpenLeg(){
        if(unlockedLeg == false){
            folderWindow.SetActive(true);
        }
        else{
            LegWindow.SetActive(true);
        }
    }


    public void LaunchEngine(){
        requestHelpPopup.SetActive(false);
        computerScreen.SetActive(false);
        startScreenBig.SetActive(false);
        endScreen.SetActive(false);
        startScreen.SetActive(true);
        engine.SetActive(true);

    }



    public void launchGame(){        
        startScreenBig.SetActive(true);
        computerScreen.SetActive(false);
    }

    public void StartButton(){
        if(playAction == menuOptions.quit){
            computerScreen.SetActive(true);
        }
        else if(playAction == menuOptions.play){
            startScreen.SetActive(false);
            errorCodeHandler.removeStartError();
        }
        else if (playAction == menuOptions.settings){
            openSetting();
        }
    }

    public void SettingsButton(){
        if(settingsAction == menuOptions.quit){
            computerScreen.SetActive(true);
        }
        else if(settingsAction == menuOptions.play){
            startScreen.SetActive(false);
        }
        else if (settingsAction == menuOptions.settings){
            openSetting();
        }
    }

    public void ExitButton(){
        if(quitAction == menuOptions.quit){
            computerScreen.SetActive(true);
        }
        else if(quitAction == menuOptions.play){
            startScreen.SetActive(false);
        }
        else if (quitAction == menuOptions.settings){
            openSetting();
        }
    }

    public void openSetting(){
        settingScreen.SetActive(true);
    }

    public void closeSetting(){
        settingScreen.SetActive(false);
    }

    public void quitEngine(){
        engine.SetActive(false);
        folderWindow.SetActive(false);
        computerScreen.SetActive(true);
    }

    public void StartGame(){
        startScreen.SetActive(false);
    }








}
