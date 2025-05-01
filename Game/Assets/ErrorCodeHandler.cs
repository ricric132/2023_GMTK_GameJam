using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorCodeHandler : MonoBehaviour
{
    public GameObject startError;
    public GameObject moveError;
    public GameObject jumpError;
    public GameObject exitError;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeStartError(){
        startError.SetActive(false);
    }

    public void removeMoveError(){
        moveError.SetActive(false);
    }

    public void removeJumpError(){
        jumpError.SetActive(false);
    }

    public void removeExitError(){
        exitError.SetActive(false);
    }

}
