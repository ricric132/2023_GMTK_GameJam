using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscordNotifs : MonoBehaviour
{
    public List<GameObject> allNotifs = new List<GameObject>();
    public float notifIntervals;
    public float duration;
    GameObject selected;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(recieveNotif());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator recieveNotif(){
        while (true){
            yield return new WaitForSeconds(notifIntervals);
            selected = allNotifs[Random.Range(0, allNotifs.Count)];
            selected.SetActive(true);
            yield return new WaitForSeconds(duration);
            selected.SetActive(false);

        }
    }

}
