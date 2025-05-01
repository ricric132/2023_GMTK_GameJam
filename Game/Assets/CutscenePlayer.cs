using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutscenePlayer : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject screen;
    
    
    void Start(){vid.loopPointReached += CheckOver;}
    
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        Destroy(screen);
        Destroy(gameObject);
    }
}
