using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public static class WaitFor
{
    public static IEnumerator FFrames(int frameCount)
    {
        if (frameCount <= 0)
        {
            throw new ArgumentOutOfRangeException("frameCount", "Cannot wait for less that 1 frame");
        }

        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }
    public static IEnumerator AnimationPos(GameObject AniObj, float yPos, bool down)
    {
        if(down){
        while (AniObj.transform.position.y < yPos)
        {
            yield return null;
        }}
        else{
        while (AniObj.transform.position.y > yPos)
        {
            yield return null;
        }}
    }

    public static IEnumerator VideoPrepared(VideoPlayer VideoObj)
    {
        while (!VideoObj.isPrepared)
        {
            yield return null;
        }
    }

    public static IEnumerator VideoPlayedTime(VideoPlayer VideoObj,float time)
    {
        while (VideoObj.time<time)
        {
            yield return null;
        }
    }
}

public static class  VideoUtil{
public static void playSpriteVideo(string videoSpriteName,bool isLooping){

    GameObject videoSprite = GameObject.Find(videoSpriteName);
    var videoPlayer = videoSprite.GetComponent<UnityEngine.Video.VideoPlayer>();
    videoPlayer.isLooping = true;
    videoPlayer.Play();
    videoSprite.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);  // may need wait for 0.1 sec to play video, otherwise the sprite will show first and there will flash.
}
public static void stopSpriteVideo(string videoSpriteName){
        GameObject camera = GameObject.Find(videoSpriteName);
        camera.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        camera.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
}
public static void stopCameraVideo(string videoSpriteName){
        GameObject camera = GameObject.Find(videoSpriteName);
        camera.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
}
public static void playCameraVideo(string videoSpriteName, bool isLooping){
        GameObject camera = GameObject.Find(videoSpriteName);
        camera.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
}
public static void playTransitionAnimation(){
        GameObject tranBottom = GameObject.Find("Transition Bottom"); //TODO ADD RULE 
        GameObject tranTop= GameObject.Find("TransitionTop"); //TODO ADD RULE 
        tranTop.GetComponent<Animator>().Play("TRANSITION",0,0.0f);
        tranBottom.GetComponent<Animator>().Play("TRANSITION2",0,0.0f);
        // yield return  WaitFor.AnimationPos(tranBottom,-3.11f,true); //TODO change transition 
}

}

public static class SliderUtil{
    public static void setSliderValues(Dictionary<string, int>cards_number_map  ){
        foreach (KeyValuePair<string, int> card_num in cards_number_map)
        {
            string num_obj = card_num.Key.ToLower() + "_num";
            string slider_obj = card_num.Key.ToLower() + "_slider";
            numSetter slider = GameObject.Find(slider_obj).GetComponent<numSetter>();
            slider.slider.value = (float)(card_num.Value - 0) / (100);
            GameObject.Find(num_obj).GetComponent<TextMeshProUGUI>().text = card_num.Value.ToString();

        }            
        int totalNum = SliderUtil.getTotalNum();
            
            GameObject.Find("total_num").GetComponent<TextMeshProUGUI>().text =  totalNum.ToString();
    }
    public static int getTotalNum(){// never use slider_value to get real card number except when set it
        POOL pool = GameObject.Find("POOL").GetComponent<POOL>();
        int total_num= 0;
        foreach (KeyValuePair<string, int> cardspairt in pool.cards_number_map_orig)
        {
            string card = cardspairt.Key;
            string num_obj = card.ToLower() + "_num";
            int card_num = System.Int32.Parse(GameObject.Find(num_obj).GetComponent<TextMeshProUGUI>().text );
            total_num+= card_num;
        }
        return total_num;

    }


}