using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag2 : MonoBehaviour
{
    public GameObject player;
    public GameObject EndScreen;
    public ErrorCodeHandler errorCodeHandler;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 20){
            EndScreen.SetActive(true);
            errorCodeHandler.removeJumpError();
        }
    }
}
