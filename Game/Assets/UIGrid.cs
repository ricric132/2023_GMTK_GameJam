using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGrid : MonoBehaviour
{
    
    List<GameObject> allTiles = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform){
            allTiles.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform findClosestEmpty(Vector2 pos){
        float closestDist = Mathf.Infinity;
        Transform closestTransform = null;
        foreach(GameObject tile in allTiles){
            if(tile.transform.childCount < 1)
            {
                float currentDist = Vector2.Distance(pos, tile.transform.position);
                if(currentDist < closestDist){
                    closestDist = currentDist;
                    closestTransform = tile.transform;
                }
            }
        }

        return closestTransform;
    }
}
