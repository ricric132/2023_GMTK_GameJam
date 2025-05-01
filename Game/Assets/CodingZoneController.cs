using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingZoneController : MonoBehaviour
{
    public List<GameObject> scripts;
    public GameObject player;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectScript(int num){
        
        foreach(GameObject gameObject in scripts){
            gameObject.GetComponent<GridManager>().PutAway();
            gameObject.SetActive(false);
        }
        
        scripts[num].SetActive(true);
        player.GetComponent<CharacterControllerScript>().gridManager = scripts[num].GetComponent<GridManager>();
        player.GetComponent<CharacterControllerScript>().pos = new Vector2Int(-1, -1);
        player.GetComponent<CharacterControllerScript>().prevPos = new List<Vector2Int>();

    }
}
