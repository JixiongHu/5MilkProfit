using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using TMPro;

public class POOL : MonoBehaviour
{
    public string[] cards;
    public Dictionary<string, int> cards_number_map = new Dictionary<string, int>();
    public Dictionary<string, int> cards_number_map_orig = new Dictionary<string, int>();

    // game setting
    int number_of_card_types;

    public int GOLDBAR;
    public int DRAW;
    public int SHERIFF;
    public int LASSO;
    public int MOONSHINE;
    public int STICKEMUP;
    public int GOLDSTASH;
    public int BANK;

    public bool gold_claim;

    // default
    //  public int GOLDBAR_orig=30;
    //  public int DRAW_orig=3;
    //  public int SHERIFF_orig=3 ;
    //  public int LASSO_orig=3;
    //  public int MOONSHINE_orig=3;
    //  public int STICKEMUP_orig=3;
    //  public int GOLDSTASH_orig =3;
    //  public int BANK_orig=3;
    // round tracker

    public string current_card_flipped = "CARDBACK0";
    public string current_card_flipped_face_name = "";
    public int num_gold_on_the_table;
    public int num_goldstash_on_the_table;

    public string[] goldcards_on_the_table;
    public string[] goldstashcards_on_the_table;
    public int number_of_card_flipped_on_the_table;


    // game tracker
    public int player1_score;
    public int player2_score;
    public int total_number_unflipped_in_pool;

    public bool card_clickable = true;
    public bool startWithCARDBACK1 = true; //TODO: when finish game 
    public int totalgolds; // for test purpose, total golds should not change

    void UpdateUnflippedCardTracker()
    {
        GOLDBAR = cards_number_map["GOLDBAR"];
        DRAW = cards_number_map["DRAW"];
        SHERIFF = cards_number_map["SHERIFF"];
        LASSO = cards_number_map["LASSO"];
        MOONSHINE = cards_number_map["MOONSHINE"];
        STICKEMUP = cards_number_map["STICKEMUP"];
        GOLDSTASH = cards_number_map["GOLDSTASH"];
        BANK = cards_number_map["BANK"];
    }
    void StartSetCardsNumber()
    {


        cards_number_map["GOLDBAR"] = cards_number_map_orig["GOLDBAR"];
        cards_number_map["DRAW"] = cards_number_map_orig["DRAW"];
        cards_number_map["LASSO"] = cards_number_map_orig["LASSO"];
        cards_number_map["SHERIFF"] = cards_number_map_orig["SHERIFF"];
        cards_number_map["MOONSHINE"] = cards_number_map_orig["MOONSHINE"];
        cards_number_map["STICKEMUP"] = cards_number_map_orig["STICKEMUP"];
        cards_number_map["GOLDSTASH"] = cards_number_map_orig["GOLDSTASH"];
        cards_number_map["BANK"] = cards_number_map_orig["BANK"];
        UpdateUnflippedCardTracker();
        number_of_card_types = 8;
        cards = new string[number_of_card_types];
        int idx = 0;
        foreach (KeyValuePair<string, int> card_num in cards_number_map)
        {
            cards[idx] = card_num.Key;
            idx++;
        }
        total_number_unflipped_in_pool = Sum(cards_number_map);
        gold_claim = true;
        Debug.Log("totalnumber" + total_number_unflipped_in_pool);
    }
    void StartNewGame()
    {
        //game tracker
        number_of_card_flipped_on_the_table = 0;
        num_gold_on_the_table = 0;
        num_goldstash_on_the_table = 0;
        goldstashcards_on_the_table = new string[16] ;
        goldcards_on_the_table = new string[16] ;
        Array.Fill<string>(goldstashcards_on_the_table,"");
                Array.Fill<string>(goldcards_on_the_table,"");

        player1_score = 3;
        player2_score = 3;
        totalgolds = cards_number_map["GOLDBAR"] + 3 * cards_number_map["GOLDSTASH"] + player1_score + player2_score;
        StartCoroutine(displayScores());

    }
    private IEnumerator PlayStartVideo()
    {
        GameObject camera = GameObject.Find("StartVideo");
        camera.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        var videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        videoPlayer.targetCameraAlpha = 1.0F;
        videoPlayer.url = "Assets/HOMESCREEN.mp4";
        videoPlayer.isLooping = true;
        videoPlayer.playOnAwake = true;
        // its prepareCompleted event.
        videoPlayer.Play();
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("StartVideoStill").SetActive(false);
        camera.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);




        WInClick rulebutton = GameObject.Find("RuleButton").GetComponent<WInClick>();
        WInClick playbutton = GameObject.Find("PlayButton").GetComponent<WInClick>();
        rulebutton.Show();
        playbutton.Show();

    }

    void Start()
    {
        Debug.Log("pool start");
        cards_number_map_orig["GOLDBAR"] = 30;
        cards_number_map_orig["DRAW"] = 3;
        cards_number_map_orig["LASSO"] = 3;
        cards_number_map_orig["SHERIFF"] = 3;
        cards_number_map_orig["MOONSHINE"] = 3;
        cards_number_map_orig["STICKEMUP"] = 3;
        cards_number_map_orig["GOLDSTASH"] = 3;
        cards_number_map_orig["BANK"] = 10;
        foreach (KeyValuePair<string, int> card_num in cards_number_map_orig){
            string num_obj = card_num.Key.ToLower()+"_num";
                    Debug.Log(num_obj);
                    GameObject.Find(num_obj).GetComponent<TextMeshProUGUI>().text=card_num.Value.ToString();
        }
       
        StartSetCardsNumber();
        StartNewGame();
        StartCoroutine(PlayStartVideo());
    }


    public void Reset()
    {
        Debug.Log("reset game");
        StartSetCardsNumber();
        StartNewGame();
        for (int i = 0; i < 16; i++)
        {
            string objname = "CARDBACK" + (i + 1).ToString();

            GameObject card_obj = GameObject.Find(objname);
            CLICK card_obj_comp = card_obj.GetComponent<CLICK>();
            card_obj_comp.Reset();
            card_obj_comp.updateColor();
        }
    }

    int Sum(Dictionary<string, int> cards_n)
    {
        int t = 0;

        foreach (KeyValuePair<string, int> card_num in cards_number_map)
        {
            t += card_num.Value;
            // Debug.Log(cards_n[i]);
        }
        return t;
    }
    void RemoveCard(string card)
    {
        cards_number_map[card] -= 1;
        total_number_unflipped_in_pool = Sum(cards_number_map);
    }
    public string generateCard()
    {
        Debug.Log("generate card: totalnumber" + total_number_unflipped_in_pool);
        total_number_unflipped_in_pool = Sum(cards_number_map);
        if (total_number_unflipped_in_pool == 0)
        {
            // GameFinish

        }
        Random r = new Random();
        int r_num = r.Next(1, total_number_unflipped_in_pool + 1);
        // Debug.Log("r_num"+r_num);

        int current_idx = 0;
        int this_card_idx = 0;
        for (int i = 0; i < cards.Length; ++i)
        {
            current_idx += cards_number_map[cards[i]];

            if (r_num <= current_idx)
            {
                this_card_idx = i;
                break;
            }
        }

        RemoveCard(cards[this_card_idx]);
        number_of_card_flipped_on_the_table++;
        if (cards[this_card_idx] == "GOLDBAR")
        {
            num_gold_on_the_table += 1;
        }
        if (cards[this_card_idx] == "GOLDSTASH")
        {
            num_goldstash_on_the_table += 1;
        }
        return cards[this_card_idx];
    }
    // Update is called once per frame


    void Update()
    {

    }

    private IEnumerator play_steal_video(bool ifPlayer1Win = true, float delay = 0.0f)
    {

        yield return new WaitForSeconds(delay);

        GameObject video = GameObject.Find("Video Player");


        var videoPlayer = video.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCameraAlpha = 1.0F;
        if (ifPlayer1Win)
            videoPlayer.url = "Assets/ANIMATION_STEAL_PLAYER1.mp4";
        else
        {
            videoPlayer.url = "Assets/ANIMATION_STEAL_PLAYER2.mp4";

        }
        videoPlayer.isLooping = true;
        videoPlayer.Play();
        GameObject winButtonDown = GameObject.Find("WinButtonDown");
        WInClick winButtonDown_comp = winButtonDown.GetComponent<WInClick>();
        winButtonDown_comp.Show();
        GameObject winButtonUp = GameObject.Find("WinButtonUp");
        WInClick winButtonUp_comp = winButtonUp.GetComponent<WInClick>();
        winButtonUp_comp.Show();
        if (ifPlayer1Win)
        {
            winButtonUp_comp.clickable = false;
        }
        else
        {
            winButtonDown_comp.clickable = false;
        }
    }

    public void round_finish_process()
    {
        StartCoroutine(displayScores());
        // play play again video and show the button
        if (total_number_unflipped_in_pool < 16)
        {
            Debug.Log("restart game");
            GameObject.Find("PlayAgain").GetComponent<WInClick>().Show();
            GameObject camera = GameObject.Find("PlayAgainVideo");
            var videoPlayer = camera.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
            videoPlayer.targetCameraAlpha = 1.0F;
            videoPlayer.url = "Assets/PLAYAGAIN.mp4";
            videoPlayer.isLooping = true;
            // its prepareCompleted event.
            videoPlayer.Play();
            camera.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            string play1_score_str = change_score_int_to_str(player1_score);
            string play2_score_str = change_score_int_to_str(player2_score);
            GameObject Play1Score_1 = GameObject.Find("Play1Score_1_FINAL");
            GameObject Play1Score_2 = GameObject.Find("Play1Score_2_FINAL");
            GameObject Play2Score_1 = GameObject.Find("Play2Score_1_FINAL");
            GameObject Play2Score_2 = GameObject.Find("Play2Score_2_FINAL");
            Play1Score_1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NUM_" + play1_score_str[0]);
            Play1Score_2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NUM_" + play1_score_str[1]);
            Play2Score_1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NUM_" + play2_score_str[0]);
            Play2Score_2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NUM_" + play2_score_str[1]);
            Reset();
        }

        // reset the color and clickable status of each card
        startWithCARDBACK1 = !startWithCARDBACK1;

        for (int i = 0; i < 16; i++)
        {
            string objname = "CARDBACK" + (i + 1).ToString();

            GameObject card_obj = GameObject.Find(objname);
            CLICK card_obj_comp = card_obj.GetComponent<CLICK>();
            card_obj_comp.Reset();
            card_obj_comp.updateColor();
        }
        current_card_flipped = "CARDBACK0";
        current_card_flipped_face_name = "";
        number_of_card_flipped_on_the_table = 0;
        num_gold_on_the_table = 0;
        num_goldstash_on_the_table = 0;
        goldcards_on_the_table = new string [16];
        goldstashcards_on_the_table = new string [16];
              Array.Fill<string>(goldstashcards_on_the_table,"");
                Array.Fill<string>(goldcards_on_the_table,"");
        //sanity check
        int new_totalscore = cards_number_map["GOLDBAR"] + 3 * cards_number_map["GOLDSTASH"] + player1_score + player2_score;
        Debug.Assert(new_totalscore == totalgolds);


    }
    private string change_score_int_to_str(int score)
    {
        string score_str = score.ToString();
        if (score_str.Length == 1)
        {
            return "0" + score_str;
        }
        else { return score_str; }


    }
    public void remove_gold_goldstash_card_on_the_table(string cardback_adpated_name, bool ifgoldbar){
        if(ifgoldbar){
            for(int  i =0;i<16;++i){
                if(goldcards_on_the_table[i]==cardback_adpated_name){
                    goldcards_on_the_table[i]=""; num_gold_on_the_table--;
                }
               
            }
        }
        else{
                       for(int  i =0;i<16;++i){
                if(goldstashcards_on_the_table[i]==cardback_adpated_name){
                    goldstashcards_on_the_table[i]=""; num_goldstash_on_the_table--;
                }
            } 
        }
    }
    public void steal(bool player1win)
    {
        if (player1win)
        {
            if (player2_score >= 3)
            {
                player1_score += 3;
                player2_score -= 3;
            }
            else
            {
                player1_score += player2_score;
                player2_score = 0;
            }
        }
        else
        {
            if (player1_score >= 3)
            {
                player2_score += 3;
                player1_score -= 3;
            }
            else
            {
                player2_score += player1_score;
                player1_score = 0;
            }
        }
    }
    public IEnumerator displayScores()
    {
        Debug.Log("displayScores");
        string play1_score_str = change_score_int_to_str(player1_score);
        string play2_score_str = change_score_int_to_str(player2_score);
        GameObject Play1Score_1 = GameObject.Find("Play1Score_1");
        GameObject Play1Score_2 = GameObject.Find("Play1Score_2");
        GameObject Play2Score_1 = GameObject.Find("Play2Score_1");
        GameObject Play2Score_2 = GameObject.Find("Play2Score_2");



        for (float i = 0f; i <= 180f; i += 10f)
        {
            // Play1Score_1.transform.rotation = Quaternion.Euler(0f, i, 0f);
            // Play1Score_2.transform.rotation = Quaternion.Euler(0f, i, 0f);
            // Play2Score_1.transform.rotation = Quaternion.Euler(0f, i, 0f);
            // Play2Score_2.transform.rotation = Quaternion.Euler(0f, i, 0f);

            if (i == 90f)
            {
                Play1Score_1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NUM_" + play1_score_str[0]);
                Play1Score_2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NUM_" + play1_score_str[1]);
                Play2Score_1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NUM_" + play2_score_str[0]);
                Play2Score_2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/NUM_" + play2_score_str[1]);
            }
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("displayScores end");

    }
    public void UpdateCardsStatus2(string current_card, string current_card_face_name, bool player1_win_action = false, bool player2_win_action = false)
    {
        // this function is called after a card is flipped
        // Step1: update status of each card
        // step2:  clear the gold:

        current_card_flipped = current_card;
        current_card_flipped_face_name = current_card_face_name;
        if (current_card_face_name == "GOLDBAR")
        {
            // num_gold_on_the_table already take this into account when generate the card
            goldcards_on_the_table[num_gold_on_the_table - 1] = current_card;

        }
        if (current_card_face_name == "GOLDSTASH")
        {
            goldstashcards_on_the_table[num_goldstash_on_the_table - 1] = current_card;
        }

        for (int i = 0; i < 16; i++)
        {
            string objname = "CARDBACK" + (i + 1).ToString();
            GameObject card_obj = GameObject.Find(objname);
            CLICK card_obj_comp = card_obj.GetComponent<CLICK>();
            card_obj_comp.ifAbleToFlip(current_card_flipped);
            card_obj_comp.updateColor();
        }


        bool round_finished = false;
        bool ifplaysteal_video = false;

        if (current_card == "CARDBACK16" || current_card_face_name == "SHERIFF" || (startWithCARDBACK1 && player2_win_action) || (!startWithCARDBACK1 && player1_win_action))
        {
            round_finished = true;
            Debug.Log("RoundFinish");

        }
        if (round_finished)
        {
            Debug.Log("RoundFinish");
            // clear gold 
            if (current_card_face_name == "SHERIFF")
            {
                cards_number_map["GOLDBAR"] += num_gold_on_the_table;
                cards_number_map["GOLDSTASH"] += num_goldstash_on_the_table;
                total_number_unflipped_in_pool = Sum(cards_number_map);
                number_of_card_flipped_on_the_table -= num_goldstash_on_the_table;
                number_of_card_flipped_on_the_table -= num_gold_on_the_table;

            }
            else if (player1_win_action && startWithCARDBACK1)
            {
                // player1 win player1's game
                player1_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
                num_gold_on_the_table = 0;
                num_goldstash_on_the_table = 0;
                //steal from the other play;
                if (current_card == "CARDBACK16")
                {
                    ifplaysteal_video = true;
                    StartCoroutine(play_steal_video(true));

                }
            }
            else if (player2_win_action && startWithCARDBACK1)
            {
                // player2 win player1's game

                player2_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
                num_gold_on_the_table = 0;
                num_goldstash_on_the_table = 0;
            }
            else if (player1_win_action && (!startWithCARDBACK1))
            {
                player1_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
                num_gold_on_the_table = 0;
                num_goldstash_on_the_table = 0;
            }
            else if ((player2_win_action) && (!startWithCARDBACK1))
            {
                player2_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
                num_gold_on_the_table = 0;
                num_goldstash_on_the_table = 0;
                //steal from the other play;
                if (current_card == "CARDBACK16")
                {
                    ifplaysteal_video = true;

                    StartCoroutine(play_steal_video(false));

                }
            }
            else
            { //finish with neither action card nor sheriff
                if (startWithCARDBACK1)
                {
                    // player1 win player1 game

                    player1_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
                    Debug.Log("|player1_score"+ player1_score);
                    num_gold_on_the_table = 0;
                    num_goldstash_on_the_table = 0;
                    ifplaysteal_video = true;

                    StartCoroutine(play_steal_video(true, 1.0f));


                }
                else
                {
                    player2_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
                    num_gold_on_the_table = 0;
                    num_goldstash_on_the_table = 0;
                    ifplaysteal_video = true;

                    StartCoroutine(play_steal_video(false, 1.0f));

                }
            }
        }
        else if (current_card_face_name == "BANK")
        {
            Debug.Log("Bank");
            if (!gold_claim)
            {
                if (startWithCARDBACK1)
                {
                    player1_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
                }
                else
                {
                    player2_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;

                }
                num_gold_on_the_table = 0;
                num_goldstash_on_the_table = 0;
            }
            else
            {
                for (int i = 0; i <  16; ++i)
                {  if(goldcards_on_the_table[i]!=""){
                    string objname = CLICK.cardback_name_adapted_to_objname(goldcards_on_the_table[i]);
                    Debug.Log(objname);
                                        GameObject.Find(objname).GetComponent<CLICK>().claimable=true;
}
                }
                                for (int i = 0; i <  16; ++i)
                {
                    if(goldstashcards_on_the_table[i]!=""){
                    string objname = CLICK.cardback_name_adapted_to_objname(goldstashcards_on_the_table[i]);
                    Debug.Log(objname);
                    GameObject.Find(objname).GetComponent<CLICK>().claimable=true;

                    }
                }




            }

        }
        else if (player1_win_action)
        {
            // if win with action card but game continue
             if (!gold_claim)
            {
            player1_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
            num_gold_on_the_table = 0;
            num_goldstash_on_the_table = 0;
            goldstashcards_on_the_table = new string [16];
            goldcards_on_the_table = new string [16];
            Array.Fill<string>(goldstashcards_on_the_table,"");
                        Array.Fill<string>(goldcards_on_the_table,"");}
            else{
                 for (int i = 0; i <  16; ++i)
                {  if(goldcards_on_the_table[i]!=""){
                    string objname = CLICK.cardback_name_adapted_to_objname(goldcards_on_the_table[i]);
                    Debug.Log(objname);
                    GameObject.Find(objname).GetComponent<CLICK>().claimable=true;
}
                }
                for (int i = 0; i <  16; ++i)
                {
                    if(goldstashcards_on_the_table[i]!=""){
                    string objname = CLICK.cardback_name_adapted_to_objname(goldstashcards_on_the_table[i]);
                    Debug.Log(objname);
                    GameObject.Find(objname).GetComponent<CLICK>().claimable=true;

                    }
                }


            }



        }
        else if (player2_win_action)
        {
            // if win with action card but game continue
             if (!gold_claim){
            player2_score += num_gold_on_the_table + 3 * num_goldstash_on_the_table;
            num_gold_on_the_table = 0;
            num_goldstash_on_the_table = 0;
                        goldstashcards_on_the_table = new string [16];
            goldcards_on_the_table = new string [16];
            Array.Fill<string>(goldstashcards_on_the_table,"");
                        Array.Fill<string>(goldcards_on_the_table,"");}
            else{
                 for (int i = 0; i <  16; ++i)
                {  if(goldcards_on_the_table[i]!=""){
                    string objname = CLICK.cardback_name_adapted_to_objname(goldcards_on_the_table[i]);
                    Debug.Log(objname);
                    GameObject.Find(objname).GetComponent<CLICK>().claimable=true;
}
                }
                for (int i = 0; i <  16; ++i)
                {
                    if(goldstashcards_on_the_table[i]!=""){
                    string objname = CLICK.cardback_name_adapted_to_objname(goldstashcards_on_the_table[i]);
                    Debug.Log(objname);
                    GameObject.Find(objname).GetComponent<CLICK>().claimable=true;

                    }
                }

            }
        }
        else
        {  //gold/gold stash continue
        }
        
        
        
        
        
        UpdateUnflippedCardTracker();
        if (!round_finished)
        {
            StartCoroutine(displayScores());
        }
        else
        {
            if (!ifplaysteal_video)
            {
                round_finish_process();
            }
        }

        Debug.Log("player1_score" + player1_score);
        Debug.Log("player2_score" + player2_score);
        Debug.Log("number_of_card_flipped_on_the_table" + number_of_card_flipped_on_the_table);
        Debug.Log("gold_on_the_table" + num_gold_on_the_table);
        Debug.Log("goldstash_on_the_table" + num_goldstash_on_the_table);

    }
}

