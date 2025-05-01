using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridObject : MonoBehaviour, IDragHandler, IDropHandler
{
    public GridManager gridManager;
    public int defaultX; 
    public int defaultY;
    public int x;
    public int y;
    public bool movable;
    bool slideTo;
    public List<Vector2Int> prevPos = new List<Vector2Int>();
    public enum BlockType{
        num,
        speed,
        jump,

        start,
        setting,
        exit,
        flag,

        level1,
        level2,
        end,

        close,

        arrow,
        add,
        minus,
        
        None

    }
    
    public int num;

    public BlockType type;
    public bool activated;

    public Animator anim;

    void Start(){
        ResetToDefault();
    }

    void Update(){
        if(type != BlockType.None){
            anim.SetBool("Active", activated);
        }
        if(slideTo){
            transform.position = Vector3.MoveTowards(transform.position, gridManager.getWorldPosition(x, y), 5f * Time.deltaTime);
            if(Vector2.Distance(transform.position, gridManager.getWorldPosition(x, y)) < 0.2f)
            {
                snapToPos();
                slideTo = false;
                gridManager.UpdateGrid();
            }
        }
    }

    public void OnCollide(){

    }

    public void snapToPos(){
        Debug.Log(x+ " " + y);
        transform.position = gridManager.getWorldPosition(x, y);
    }

    public void slideToPos(Vector2Int coords){
        x = coords.x;
        y = coords.y;
        slideTo = true;
        
    }

    public void savePos(){
        prevPos.Add(new Vector2Int(x, y));
    }

    public void rewind(){
        Debug.Log("count ="+  prevPos.Count + movable);
        gridManager.grid[x, y] = null;
        x = prevPos[prevPos.Count - 1].x;
        y = prevPos[prevPos.Count - 1].y;
        gridManager.grid[x, y] = gameObject;
        prevPos.RemoveAt(prevPos.Count - 1);
        snapToPos();
    }

    public void OnDrag(PointerEventData eventData){
        //Vector2Int coords = gridManager.getXY(eventData.position);
        //transform.position = gridManager.getWorldPosition(coords.x, coords.y);
        transform.position = eventData.position;

    }

    public void OnDrop(PointerEventData eventData){
        Vector2Int coords = gridManager.getXY(eventData.position);
        transform.position = gridManager.getWorldPosition(coords.x, coords.y);
        x = coords.x;
        y = coords.y;
        gridManager.UpdateGrid();
    }

    public void ResetToDefault(){
        x = defaultX;
        y = defaultY;
        snapToPos();
        gridManager.UpdateGrid();
    }
    


}
