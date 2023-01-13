using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Button Button;
    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(wakeUpBarsOnClick);
        
    }

    private IEnumerator play_transition_video(){
         GameObject can_bars= GameObject.Find("Canvas");
         GameObject transition_video = GameObject.Find("transition_video");
         
        var videoplayer= transition_video.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoplayer.url = "Assets/TRANSITION.mp4";
        videoplayer.Play();
        yield return new WaitForSeconds(0.2f);
                // transition_video.transform.position = new Vector3(transition_video.transform.position.x,transition_video.transform.position.y,-0.1f);

      GameObject.Find("transition_video").GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Opacity", 1.0f);
                    GameObject.Find("POOL").GetComponent<POOL>().card_clickable=false;


        yield return new WaitForSeconds(2.0f);
                    can_bars.GetComponent<Canvas>().planeDistance = 5;

        // transition_video.transform.position = new Vector3(transition_video.transform.position.x,transition_video.transform.position.y,-600f);
      GameObject.Find("transition_video").GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_Opacity", 0.0f);
        videoplayer.url = "";

    }
    void wakeUpBarsOnClick(){
        if(gameObject.name=="Button"){
            StartCoroutine(play_transition_video());
        // GameObject can_bars= GameObject.Find("Canvas");
        // GameObject.Find("transition_video").GetComponent<UnityEngine.Video.VideoPlayer>().url = "Assets/TRANSITION.mp4";
        //     can_bars.GetComponent<Canvas>().planeDistance = 5;

        // GameObject.Find("POOL").GetComponent<POOL>().card_clickable=false;
        }
        else if(gameObject.name=="ok"){
            GameObject.Find("POOL").GetComponent<POOL>().card_clickable=true;
            GameObject.Find("Canvas").GetComponent<Canvas>().planeDistance = -20;
            string [] cards =  GameObject.Find("POOL").GetComponent<POOL>().cards; // change to pool cards later
            for(int i=0;i<cards.Length;++i){
            GameObject.Find("POOL").GetComponent<POOL>().cards_number_map_orig[cards[i]] =System.Int32.Parse((GameObject.Find(cards[i].ToLower()+"_num").GetComponent<TextMeshProUGUI>().text) );
            }
            GameObject.Find("POOL").GetComponent<POOL>().Reset();
        }
        else if(gameObject.name=="cancel"){
            GameObject.Find("POOL").GetComponent<POOL>().card_clickable=true;
            GameObject.Find("Canvas").GetComponent<Canvas>().planeDistance = -20;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
