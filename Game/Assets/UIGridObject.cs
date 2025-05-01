using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGridObject : MonoBehaviour, IDragHandler, IDropHandler
{
    public UIGrid grid;
    public Transform hand;
    public enum BlockType{
        zero,
        one,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,

        speed,
        jump,
        start,
        setting,
        exit,
        flag,
        gravity,
        equal,
        add,
        minus

    }

    public BlockType type;
    public int num;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData){
        transform.parent = hand;
        Transform closestSlot = grid.findClosestEmpty(eventData.position);
        if(closestSlot != null)
        {
            transform.position = closestSlot.position;
        }
        else{
            transform.position = eventData.position;
        }
    }

    public void OnDrop(PointerEventData eventData){
        Transform closestSlot = grid.findClosestEmpty(eventData.position);
        if(closestSlot != null)
        {
            transform.position = closestSlot.position;
        }
    }
}
