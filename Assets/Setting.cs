using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Button Button;
    public bool isClickable;
    // Start is called before the first frame update
    void Start()
    {
                Debug.Log("Menu AddListener");
        isClickable =true;
        Button.onClick.AddListener(wakeUpBarsOnClick);
        
    }

    private IEnumerator showMenu(){
         GameObject can_bars= GameObject.Find("MenuCanvas");
        GameObject.Find("POOL").GetComponent<POOL>().card_clickable=false;
        yield return new WaitForSeconds(0.0f);
        can_bars.GetComponent<Canvas>().planeDistance = 5;
    }
    void wakeUpBarsOnClick(){
        Debug.Log("Menu Click");

        if(gameObject.name=="MenuButton"&& isClickable){
            Debug.Log("Menu Click");
            StartCoroutine(showMenu());
        }
        else if(gameObject.name=="ok"){

            POOL pool = GameObject.Find("POOL").GetComponent<POOL>();
            pool.card_clickable=true;
            string [] cards = pool.cards; // change to pool cards later
            int [] card_nums = new int [cards.Length];
            int total_card = 0;
            for(int i=0;i<cards.Length;++i){
                card_nums[i]=System.Int32.Parse((GameObject.Find(cards[i].ToLower()+"_num").GetComponent<TextMeshProUGUI>().text) );
                total_card+=card_nums[i];
            }
            if(total_card<100 && total_card>=16){
            for(int i=0;i<cards.Length;++i){
            GameObject.Find("POOL").GetComponent<POOL>().cards_number_map_orig[cards[i]] =card_nums[i];
            }            
            GameObject.Find("MenuCanvas").GetComponent<Canvas>().planeDistance = -20;

            GameObject.Find("POOL").GetComponent<POOL>().Reset();}
            else{
                //TODO: pop up same msg
                Debug.Log("total num "+ total_card);
            }
        }
        else if(gameObject.name=="cancel"){
            SliderUtil.setSliderValues(GameObject.Find("POOL").GetComponent<POOL>().cards_number_map_orig);
            GameObject.Find("POOL").GetComponent<POOL>().card_clickable=true;
            GameObject.Find("MenuCanvas").GetComponent<Canvas>().planeDistance = -20;
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
