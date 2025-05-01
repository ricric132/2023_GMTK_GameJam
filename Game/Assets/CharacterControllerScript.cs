using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    Vector2Int dir;
    public GridManager gridManager;
    public Vector2Int pos = Vector2Int.zero;
    public List<Vector2Int> prevPos = new List<Vector2Int>();
    public bool canMove = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove){
            return;
        }
        transform.position = new Vector2(pos.x, pos.y) * gridManager.cellSize + new Vector2(gridManager.cellSize, gridManager.cellSize)/2;
        
        if(Input.GetKeyDown(KeyCode.A)){
            saveState();
            dir = new Vector2Int(-1, 0);
            if(gridManager.checkMovable(pos + dir)){
                pos += dir;
            }
            else if(gridManager.checkObject(pos + dir) != null){
                gridManager.SlideObject(pos + dir, dir);
            } 
            
        }            
        else if(Input.GetKeyDown(KeyCode.D)){
            saveState();
            dir = new Vector2Int(1, 0);
            if(gridManager.checkMovable(pos + dir)){
                pos += dir;
            }
            else if(gridManager.checkObject(pos + dir) != null){
                gridManager.SlideObject(pos + dir, dir);
            }
        }            
        else if(Input.GetKeyDown(KeyCode.W)){
            saveState();
            dir = new Vector2Int(0, 1);
            if(gridManager.checkMovable(pos + dir)){
                pos += dir;
            }
            else if(gridManager.checkObject(pos + dir) != null){
                gridManager.SlideObject(pos + dir, dir);
            }
        }            
        else if(Input.GetKeyDown(KeyCode.S)){
            saveState();
            dir = new Vector2Int(0, -1);        
            if(gridManager.checkMovable(pos + dir)){
                pos += dir;
            }
            else if(gridManager.checkObject(pos + dir) != null){
                gridManager.SlideObject(pos + dir, dir);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Z)){
            rewindState();
        }
        else if(Input.GetKeyDown(KeyCode.R)){
            gridManager.Reset();
            pos = new Vector2Int(-1, -1);
            prevPos = new List<Vector2Int>();
            gridManager.PutAway();
        }
    }

    void saveState(){
        gridManager.SaveTilePos();
        prevPos.Add(pos);
    }
    void rewindState(){
        gridManager.RewindTilePos();        
        pos = prevPos[prevPos.Count - 1];
        prevPos.RemoveAt(prevPos.Count - 1);

    }
}
