using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WInClick : MonoBehaviour
{
    public bool clickable = false;
    public bool ifSteal = false;
    public bool isActionResult =  false;
    string opponent_click_name;

    string triggered_card_filename;
    string triggered_card_typename;
    bool player1;
    float origin_z;



    // Start is called before the first frame update

    void Start()
    {
        ifSteal = false;
        if (this.gameObject.name == "PlayButton" || this.gameObject.name == "RuleButton")
        {
            clickable = true;
        }
        else
        {
            clickable = false;
        }
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        opponent_click_name = "";
        if (this.gameObject.name == "WinButtonDown")
        {
            player1 = true;
            opponent_click_name = "WinButtonUp";
        }
        if (this.gameObject.name == "WinButtonUp")
        {
            player1 = false;
            opponent_click_name = "WinButtonDown";
        }

        if (this.gameObject.name == "PlayButton")
        {
            opponent_click_name = "RuleButton";
        }

        if (this.gameObject.name == "RuleButton")
        {
            opponent_click_name = "PlayButton";
        }

        origin_z = 3.0f;
        // this.gameObject.SetActive(false);
    }


    public void Hide()
    {
        Debug.Log(this.gameObject.name + "button hide" + origin_z);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, origin_z);
        GameObject.Find("POOL").GetComponent<POOL>().card_clickable = true;
        GameObject.Find("MenuButton").GetComponent<Button>().interactable = true;

    }
    public void Show(string the_triggered_card_filename = null, string the_triggered_card_facename = null)
    {
        Debug.Log(this.gameObject.name + "button show");
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, CLICK.position_z - 0.1f);
        triggered_card_filename = the_triggered_card_filename;
        triggered_card_typename = the_triggered_card_facename;
        clickable = true;
        GameObject.Find("POOL").GetComponent<POOL>().card_clickable = false;
        GameObject.Find("MenuButton").GetComponent<Button>().interactable = false;
        if(this.gameObject.name=="WinButtonDown"||this.gameObject.name=="WinButtonUp"){
        if (the_triggered_card_filename == null)
        {
            ifSteal = true;
        }  
        else{
            isActionResult =true;
        }
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator play_and_move(UnityEngine.Video.VideoPlayer videoPlayer,Animator top, Animator down)
    {

        videoPlayer.Prepare();
        // yield return new WaitForSeconds(0.5f); //TODO change video time
        yield return WaitFor.VideoPrepared(videoPlayer);
        videoPlayer.Play();
        GameObject.Find("homescreen_sqr").transform.position =
        new Vector3(GameObject.Find("homescreen_sqr").transform.position.x, GameObject.Find("homescreen_sqr").transform.position.y, -0.3f);
        
        yield return WaitFor.VideoPlayedTime(videoPlayer, 0.5f);   //stop previous video after previous one played 0.5    
        GameObject video1 = GameObject.Find("StartVideo");
        video1.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
        video1.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        top.Play("TRANSITION",0,0.0f);
        down.Play("TRANSITION2",0,0.0f);
        // GameObject.Find("homescreen_sqr").transform.position =
        // new Vector3(GameObject.Find("homescreen_sqr").transform.position.x, GameObject.Find("homescreen_sqr").transform.position.y, -0.3f);
    }
    private IEnumerator play_playrule_video(bool ifPlay = true)
    {
        POOL pool = GameObject.Find("POOL").GetComponent<POOL>();
        pool.card_clickable = false;

        GameObject tranBottom = GameObject.Find("Transition Bottom"); //TODO ADD RULE 
        GameObject tranTop= GameObject.Find("TransitionTop"); //TODO ADD RULE 
        GameObject video = GameObject.Find("HOMESCREEN_PLAY"); //TODO ADD RULE 
        var videoPlayer = video.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.targetCameraAlpha = 1.0F;
        videoPlayer.isLooping = false;
        StartCoroutine(play_and_move(videoPlayer,tranTop.GetComponent<Animator>(),tranBottom.GetComponent<Animator>()));


        yield return  WaitFor.AnimationPos(tranBottom,-3.11f,true); //TODO change transition 
        pool.card_clickable = true;
        videoPlayer.Stop();
        GameObject.Find("homescreen_sqr").transform.position = new Vector3(GameObject.Find("homescreen_sqr").transform.position.x, GameObject.Find("homescreen_sqr").transform.position.y, -20f);



    }
    
    private IEnumerator actionForPlayAgainButton(){
        VideoUtil.playTransitionAnimation();
        yield return  WaitFor.AnimationPos( GameObject.Find("Transition Bottom"),-3.11f,true); 
         VideoUtil.stopSpriteVideo("PlayAgainVideo");
                GameObject Play1Score_1 = GameObject.Find("Play1Score_1_FINAL");
                GameObject Play1Score_2 = GameObject.Find("Play1Score_2_FINAL");
                GameObject Play2Score_1 = GameObject.Find("Play2Score_1_FINAL");
                GameObject Play2Score_2 = GameObject.Find("Play2Score_2_FINAL");
                Play1Score_1.GetComponent<SpriteRenderer>().sprite = null;
                Play1Score_2.GetComponent<SpriteRenderer>().sprite = null;
                Play2Score_1.GetComponent<SpriteRenderer>().sprite = null;
                Play2Score_2.GetComponent<SpriteRenderer>().sprite = null;

    }
    void OnMouseDown()
    {
        Debug.Log(this.gameObject.name + "_click, clickable: " + clickable);
        if (clickable)
        {
            if (this.gameObject.name == "PlayAgainButton")
            {
                StartCoroutine(actionForPlayAgainButton());
            }
            else if (this.gameObject.name == "PlayButton" || this.gameObject.name == "RuleButton")
            {
                if (this.gameObject.name == "PlayButton")
                {
                    //play video
                    StartCoroutine(play_playrule_video(true));

                }
                else if (this.gameObject.name == "RuleButton")
                {
                    StartCoroutine(play_playrule_video(false));
                }
            }
            else
            {
                if (ifSteal)
                {
                    if (player1)
                    {
                        VideoUtil.stopCameraVideo("STEALPLAYER1_VIDEO");
                    }
                    else
                    {
                        VideoUtil.stopCameraVideo("STEALPLAYER2_VIDEO");
                    }
                }
                else
                {// action card
                    VideoUtil.stopCameraVideo(triggered_card_typename + "_VIDEO");
                }
            }
            string the_triggered_card_filename = triggered_card_filename;
            bool the_ifSteal = ifSteal;
            bool the_isActionResult = isActionResult;

            this.Hide();
            this.Reset();
            if (opponent_click_name != "")
            {
                GameObject.Find(opponent_click_name).GetComponent<WInClick>().Hide();
                GameObject.Find(opponent_click_name).GetComponent<WInClick>().Reset();
            }
            // send click info to origin card
            Debug.Log(triggered_card_filename);


            if (the_ifSteal)
            {
                POOL pool = GameObject.Find("POOL").GetComponent<POOL>();
                if (this.gameObject.name == "WinButtonDown")
                {
                    pool.steal(true);
                }
                else
                {
                    pool.steal(false);
                }
                pool.round_finish_process();

            }
            else if(the_isActionResult)
            {
                GameObject.Find(the_triggered_card_filename).GetComponent<CLICK>().processResultsFromButton(player1);
            }


        }



    }
    void Reset()
    {
        clickable = false;
        ifSteal = false;
        isActionResult=false;
        triggered_card_filename = null;
        triggered_card_typename = null;
    }
}
