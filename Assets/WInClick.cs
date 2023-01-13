using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WInClick : MonoBehaviour
{
    public bool clickable=false;
    public bool ifSteal=false;

    string opponent_click_name;

    string triggered_card_filename;
    bool player1;
    float origin_z;



    // Start is called before the first frame update
    
    void Start()
    {
        ifSteal=false;
        if(this.gameObject.name =="PlayButton" ||this.gameObject.name =="RuleButton" ){
            clickable=true;
        }
        else{
        clickable=false;}
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255,0);
        opponent_click_name="";
        if(this.gameObject.name=="WinButtonDown"){player1=true;
            opponent_click_name="WinButtonUp";
        }
        if(this.gameObject.name=="WinButtonUp"){player1=false;
                opponent_click_name="WinButtonDown";    
        }

        if(this.gameObject.name=="PlayButton"){
            opponent_click_name="RuleButton";
        }
           
        if(this.gameObject.name=="RuleButton"){
            opponent_click_name="PlayButton";
        }

        origin_z=3.0f;
        // this.gameObject.SetActive(false);
    }
    public void setClickable(bool ifTrue){
        Debug.Log("SetClickable to "+ifTrue);
        clickable=ifTrue;
    }

    public void Hide(){
        Debug.Log(this.gameObject.name+"button hide"+origin_z);
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, origin_z);
        GameObject.Find("POOL").GetComponent<POOL>().card_clickable=true;
    }
    public void Show(string the_triggered_card=null){
        Debug.Log(this.gameObject.name+"button show");
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, CLICK.position_z-0.1f);
        triggered_card_filename=the_triggered_card;
        Debug.Log(triggered_card_filename);
        clickable=true;
        GameObject.Find("POOL").GetComponent<POOL>().card_clickable=false;
        if(the_triggered_card==null){
            ifSteal=true;
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    private IEnumerator play_and_move(UnityEngine.Video.VideoPlayer videoPlayer){
        videoPlayer.Play();
        yield return new WaitForSeconds(0.5f); //TODO change video time
                GameObject video1 = GameObject.Find("StartVideo");
                     video1.GetComponent<UnityEngine.Video.VideoPlayer>().url="";
        video1.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,0.0f);

        GameObject.Find("homescreen_sqr").transform.position =  
        new Vector3(GameObject.Find("homescreen_sqr").transform.position .x,GameObject.Find("homescreen_sqr").transform.position .y, -0.3f);
    }
    private IEnumerator play_playrule_video(bool ifPlay=true){
        POOL pool = GameObject.Find("POOL").GetComponent<POOL>();
        pool.card_clickable=false;
        
        GameObject video = GameObject.Find("Video Player2");
        
            var videoPlayer = video.GetComponent<UnityEngine.Video.VideoPlayer>();
            // videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
            videoPlayer.targetCameraAlpha = 1.0F;
            if(ifPlay)
            videoPlayer.url = "Assets/HOMESCREEN_PLAY.mp4";
            else{
            videoPlayer.url = "Assets/HOMESCREEN_RULE.mp4";
            }
            videoPlayer.isLooping = false;
        

        StartCoroutine(play_and_move(videoPlayer));

        // videoPlayer.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,1.0f);

        yield return new WaitForSeconds(2.0f); //TODO change video time
        pool.card_clickable=true;
        videoPlayer.url="";
        GameObject.Find("homescreen_sqr").transform.position =  new Vector3(GameObject.Find("homescreen_sqr").transform.position .x,GameObject.Find("homescreen_sqr").transform.position .y, -20f);
        GameObject video1 = GameObject.Find("StartVideo");

   

    }
    void OnMouseDown(){
        Debug.Log(this.gameObject.name+"_click"+clickable);
        if(clickable){
            if(this.gameObject.name=="PlayAgain"){
                    GameObject camera = GameObject.Find("PlayAgainVideo");
                    camera.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,0.0f);
                    camera.GetComponent<UnityEngine.Video.VideoPlayer>().url=null;

                }
            else if(this.gameObject.name=="PlayButton"||this.gameObject.name=="RuleButton"){
                    // GameObject camera = GameObject.Find("PlayAgainVideo");
                    // camera.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,0.0f);
                    // camera.GetComponent<UnityEngine.Video.VideoPlayer>().url=null;
                }            
            else{
                GameObject camera = GameObject.Find("Video Player");
                var videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
                videoPlayer.url=null;
                // if(this.gameObject.name!="PlayButton"&&this.gameObject.name!="RuleButton")
                // {                  

                //     videoPlayer.url=null;
                //         Debug.Log("destroyvideo");
                //     }
                }

            this.Hide();
            string the_triggered_card_filename=triggered_card_filename;
            bool the_ifSteal=ifSteal;
            this.Reset();
            if(opponent_click_name!=""){
            GameObject.Find(opponent_click_name).GetComponent<WInClick>().Hide();
            GameObject.Find(opponent_click_name).GetComponent<WInClick>().Reset();}
            // send click info to origin card
            Debug.Log(triggered_card_filename);
            if(this.gameObject.name=="PlayButton"){
                //play video
                StartCoroutine(play_playrule_video(true));

            }
            else if(this.gameObject.name=="RuleButton"){
                StartCoroutine(play_playrule_video(false));
            }
            else if (this.gameObject.name=="PlayAgain"){
                GameObject Play1Score_1=GameObject.Find("Play1Score_1_FINAL");
                GameObject Play1Score_2=GameObject.Find("Play1Score_2_FINAL");
                GameObject Play2Score_1=GameObject.Find("Play2Score_1_FINAL");
                GameObject Play2Score_2=GameObject.Find("Play2Score_2_FINAL");
                Play1Score_1.GetComponent<SpriteRenderer>().sprite =  null;
                Play1Score_2.GetComponent<SpriteRenderer>().sprite =   null;
                Play2Score_1.GetComponent<SpriteRenderer>().sprite =   null;
                Play2Score_2.GetComponent<SpriteRenderer>().sprite =   null;
            }
            else if(the_ifSteal){
                POOL pool = GameObject.Find("POOL").GetComponent<POOL>();
                if(this.gameObject.name=="WinButtonDown"){
                    pool.steal(true); 
                }
                else{               
                    pool.steal(false); 
}
                
                pool.round_finish_process();
                
            }
            else{
                GameObject.Find(the_triggered_card_filename).GetComponent<CLICK>().processResultsFromButton(player1);
            }


        }
        


    }
    void Reset(){
        clickable=false;
        ifSteal=false;
        triggered_card_filename=null;
    }
}
