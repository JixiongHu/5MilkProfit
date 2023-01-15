using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CLICK : MonoBehaviour
{
    bool isClicked;  
    public static float position_z = 0.0f;  
    [SerializeField]

    public Sprite Back, GOLDBAR, DRAW, LASSO, SHERIFF, MOONSHINE, STICKEMUP, GOLDSTASH, BANK, BLANK_CARD;
    private SpriteRenderer rend;
    private Dictionary<string,Sprite> sprite_dict = new Dictionary<string, Sprite> () ;
    bool coroutineAllowed = true;
    public bool claimable =false;
    public bool facedUp=false;
    public bool flipable=false;
    public bool isClaimed=false;
    public string card_face_name;
    
    private string this_card_name_adapted; //
    private string get_this_card_name_adpated(bool ifStart=false){
        if(ifStart){
            return this.gameObject.name;
        }
        else{
        GameObject pool_obj= GameObject.Find("POOL");
        POOL pool_obj_comp= pool_obj.GetComponent<POOL>();
        if(pool_obj_comp.startWithCARDBACK1){
            return this_card_name_adapted=this.gameObject.name;
        }
        else{
                int this_card_index;
        if(this.gameObject.name.Length==9){this_card_index=System.Int32.Parse(this.gameObject.name.Substring(8,1));}
        else {this_card_index=System.Int32.Parse(this.gameObject.name.Substring(8,2));}
        return "CARDBACK"+(17-this_card_index).ToString();
        }}
        }
    public static string cardback_name_adapted_to_objname(string cardbackname){
        GameObject pool_obj= GameObject.Find("POOL");
    POOL pool_obj_comp= pool_obj.GetComponent<POOL>();
    if(pool_obj_comp.startWithCARDBACK1){
        return cardbackname;
    }


    else{    int this_card_index;
    if(cardbackname.Length==9){this_card_index=System.Int32.Parse(cardbackname.Substring(8,1));}
    else {this_card_index=System.Int32.Parse(cardbackname.Substring(8,2));}
    return "CARDBACK"+(17-this_card_index).ToString();
    }
}
    public void setFlipable(bool ifflipable){
        flipable=ifflipable;
    }
    // Start is called before the first frame update
    void Start_Load_Sprite(){
        Back= Resources.Load<Sprite>("Sprites/CARD BACK ROUNDED");
        GOLDBAR= Resources.Load<Sprite>("Sprites/GOLD BAR ROUNDED");
        DRAW= Resources.Load<Sprite>("Sprites/DRAW ROUNDED");
        LASSO= Resources.Load<Sprite>("Sprites/LASSO ROUNDED");
        SHERIFF= Resources.Load<Sprite>("Sprites/SHERIFF ROUNDED");
        MOONSHINE= Resources.Load<Sprite>("Sprites/MOONSHINE ROUNDED");
        STICKEMUP= Resources.Load<Sprite>("Sprites/STICKEMUP ROUNDED");
        GOLDSTASH= Resources.Load<Sprite>("Sprites/GOLD STASH ROUNDED");
        BLANK_CARD =  Resources.Load<Sprite>("Sprites/BLANK_CARD");
        BANK= Resources.Load<Sprite>("Sprites/BANK ROUNDED"); 
        sprite_dict["GOLDBAR"]=GOLDBAR;
        sprite_dict["DRAW"]=DRAW;
        sprite_dict["LASSO"]=LASSO;
        sprite_dict["SHERIFF"]=SHERIFF;
        sprite_dict["MOONSHINE"]=MOONSHINE;
        sprite_dict["STICKEMUP"]=STICKEMUP;
        sprite_dict["GOLDSTASH"]=GOLDSTASH;
        sprite_dict["BANK"]=BANK;
    }
    void StartNewRound(){
        rend = GetComponent<SpriteRenderer>();
        isClicked=false;
        facedUp = false;
        claimable =false;
        isClaimed=false;
        this_card_name_adapted=get_this_card_name_adpated();
        rend.sprite = Back;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if(this_card_name_adapted!="CARDBACK1"){
            flipable=false;
            rend.color=new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }else{            
            flipable=true;
            rend.color=new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        this.transform.position =new Vector3(this.transform.position.x,this.transform.position.y,position_z);

    }

    void Start()
    {   
       Start_Load_Sprite();
       StartNewRound();
    }
    public void Reset(){ 
        StartNewRound();
    }
    private IEnumerator RotateCard()
    {
        GameObject pool_obj= GameObject.Find("POOL");
        POOL pool_obj_comp= pool_obj.GetComponent<POOL>();
        card_face_name = pool_obj_comp.generateCard();
  
        if(card_face_name=="MOONSHINE"||card_face_name=="STICKEMUP"||card_face_name=="DRAW"||card_face_name=="SHERIFF"||card_face_name=="LASSO"){
            // SceneManager.LoadScene ("Untitled");

            // pool_obj_comp.card_clickable=false;
            GameObject video = GameObject.Find(card_face_name+"_VIDEO");
            VideoPlayer videoPlayer = video.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            videoPlayer.targetCameraAlpha = 0.0F;
            videoPlayer.isLooping = true;
            videoPlayer.Play();}
        Sprite faceSprite=sprite_dict[card_face_name];
        coroutineAllowed = false;
        pool_obj_comp.card_clickable=false;

        if (!facedUp)
        {
            for (float i = 0f; i <= 180f; i += 18f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = faceSprite;
                }
                yield return new WaitForSeconds(0.002f);
            }
        }

        else if (facedUp)
        {
            for (float i = 180f; i >= 0f; i -= 18f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = Back;
                }
                yield return new WaitForSeconds(0.002f);
            }
        }

        coroutineAllowed = true;
        facedUp = !facedUp;
        isClicked=true;

        if(card_face_name=="MOONSHINE"||card_face_name=="STICKEMUP"||card_face_name=="DRAW"||card_face_name=="SHERIFF"||card_face_name=="LASSO"){
            // SceneManager.LoadScene ("Untitled");

            // pool_obj_comp.card_clickable=false;
            GameObject video = GameObject.Find(card_face_name+"_VIDEO");
            VideoPlayer videoPlayer = video.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoPlayer.targetCameraAlpha = 1.0F;

            yield return new WaitForSeconds(0.2f);
            GameObject.Find(card_face_name+"_SFX").GetComponent<AudioSource>().Play();
            GameObject winButtonDown= GameObject.Find("WinButtonDown");
            WInClick winButtonDown_comp = winButtonDown.GetComponent<WInClick>();
            winButtonDown_comp.Show(this.gameObject.name,card_face_name);
            GameObject winButtonUp= GameObject.Find("WinButtonUp");
            WInClick winButtonUp_comp = winButtonUp.GetComponent<WInClick>();
            winButtonUp_comp.Show(this.gameObject.name,card_face_name);
        }

        else{
            //gold/bank
            pool_obj_comp.card_clickable=true;
            pool_obj_comp.UpdateCardsStatus2(this_card_name_adapted,card_face_name,false,false);

        }


    }  

    public void ifAbleToFlip(string current_card_flipped, bool startWithCARDBACK1=true){
        if(this_card_name_adapted=="CARDBACK1"){
            if(current_card_flipped=="CARDBACK0")
            {flipable=true;}
            else {flipable=false;}
        }
         int current_card_flipped_idx;
        if(current_card_flipped.Length==9){current_card_flipped_idx=System.Int32.Parse(current_card_flipped.Substring(8,1));}
        else {current_card_flipped_idx=System.Int32.Parse(current_card_flipped.Substring(8,2));}
        Dictionary<int,int []> map_from_current_to_ableToFlip = new Dictionary<int, int[]>();
        map_from_current_to_ableToFlip[1]=new int [] {2,3};
        map_from_current_to_ableToFlip[2]=new int [] {3,4,5};
        map_from_current_to_ableToFlip[3]=new int [] {2,5,6};
        map_from_current_to_ableToFlip[4]=new int [] {5,7,8};
        map_from_current_to_ableToFlip[5]=new int [] {4,6, 8,9};
        map_from_current_to_ableToFlip[6]=new int [] {5,9,10};
        map_from_current_to_ableToFlip[7]=new int [] {8,11};
        map_from_current_to_ableToFlip[8]=new int [] {7,9, 11,12};
        map_from_current_to_ableToFlip[9]=new int [] {8,10, 12,13};
        map_from_current_to_ableToFlip[10]=new int [] {9,13};
        map_from_current_to_ableToFlip[11]=new int [] {12,14};
        map_from_current_to_ableToFlip[12]=new int [] {11,13,14,15};
        map_from_current_to_ableToFlip[13]=new int [] {12,15};
        map_from_current_to_ableToFlip[14]=new int [] {15,16};
        map_from_current_to_ableToFlip[15]=new int [] {14,16};
        map_from_current_to_ableToFlip[16]=new int [] {};
        int [] cards_able_to_flip = map_from_current_to_ableToFlip[current_card_flipped_idx];
        bool inTheList = false;
        for(int i=0;i<cards_able_to_flip.Length;++i){
            if(this_card_name_adapted=="CARDBACK"+ cards_able_to_flip[i].ToString()){
                inTheList=true;
                break;
            }
        }
        flipable=inTheList;
    }

    public void updateColor(){
        if((isClicked||flipable)&&(!isClaimed)){
                        rend.color=new Color(255,255,255,255);
        
        }
        else{
                        rend.color=new Color(1.0f, 1.0f, 1.0f, 0.5f);

        }
    }
    void OnMouseDown()
    {
    Debug.Log("mouse down");
    
    // if(Input.GetMouseButtonDown(0)){
        if (coroutineAllowed)
        {   
            coroutineAllowed=false;
             GameObject pool_obj= GameObject.Find("POOL");
            POOL pool_obj_comp= pool_obj.GetComponent<POOL>();
            if(isClicked==false&&flipable==true&&pool_obj_comp.card_clickable){
                Debug.Log("flip"+ this.gameObject.name);
                isClicked=true;
                StartCoroutine(RotateCard());
            }
            else if(isClicked&& claimable&&facedUp&&(card_face_name=="GOLDBAR"||card_face_name=="GOLDSTASH")&&pool_obj_comp.card_clickable){
                Debug.Log("get gold"+ this.gameObject.name);
                int score;
                if(card_face_name=="GOLDBAR"){
                    score =1;
                    GameObject.Find("CLAIMGOLD_SFX").GetComponent<AudioSource>().Play();

                }
                else{
                    score=3;
                    GameObject.Find("CLAIMSTASH_SFX").GetComponent<AudioSource>().Play();

                }
                POOL pool= GameObject.Find("POOL").GetComponent<POOL>();
                if(pool.startWithCARDBACK1){
                    pool.player1_score += score;
                StartCoroutine(pool.displayScores());

                }
                else{
                    pool.player2_score+=score;
                    StartCoroutine(pool.displayScores());
                }

                // change card to blank card 
                GameObject.Find(this.gameObject.name+"_GOLDSTASH").GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,0.0f);
                GameObject.Find(this.gameObject.name+"_GOLDBAR").GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,0.0f);

 
        GameObject.Find(this.gameObject.name+"_GOLDSTASH").GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
                
        GameObject.Find(this.gameObject.name+"_GOLDBAR").GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
    
//   GameObject.Find(objname+"_").GetComponent<UnityEngine.Video.VideoPlayer>().Play();

                rend.color = new Color(1.0f,1.0f,1.0f,0.5f);

                if(card_face_name=="GOLDBAR"){
                    pool.remove_gold_goldstash_card_on_the_table(this.this_card_name_adapted,true);
                }
                else{
                    pool.remove_gold_goldstash_card_on_the_table(this.this_card_name_adapted,false);
                }
                claimable=false;
                isClaimed=true;
                // isClicked=false;

            }
            else{
       
            }}
            coroutineAllowed=true;
        }
 
    //     }
    
    void Update()
    {
        
    
    }

    public void processResultsFromButton(bool player1_win_action){
        GameObject pool_obj= GameObject.Find("POOL");
        POOL pool_obj_comp= pool_obj.GetComponent<POOL>();
        pool_obj_comp.UpdateCardsStatus2(this_card_name_adapted,card_face_name,player1_win_action,!player1_win_action);
}
}
