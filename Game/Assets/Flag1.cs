using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag1 : MonoBehaviour
{
    public GameObject player;
    public Transform TPlocation;
    public ErrorCodeHandler errorCodeHandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 5){
            player.transform.position = TPlocation.transform.position;
            errorCodeHandler.removeMoveError();
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawWireSphere(transform.position, 5);
    }

}
