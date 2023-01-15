using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class test : MonoBehaviour
{
    bool updated=true;
    int frame =0 ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {

        frame++;
        if(frame>1000){
        GameObject.Find("HOMESCREEN_RULES").GetComponent<VideoPlayer>().Play();}


    }
}
