using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardsNumModeTickBox : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string,Dictionary<string, int>> cards_number_map_orig_differentMode = new Dictionary<string,Dictionary<string, int>>();

    void Start()
    {   
        cards_number_map_orig_differentMode["ClassicTick"] = new Dictionary<string, int>();
        cards_number_map_orig_differentMode["ClassicTick"]["GOLDBAR"] = 30;
        cards_number_map_orig_differentMode["ClassicTick"]["DRAW"] = 3;
        cards_number_map_orig_differentMode["ClassicTick"]["LASSO"] = 3;
        cards_number_map_orig_differentMode["ClassicTick"]["SHERIFF"] = 8;
        cards_number_map_orig_differentMode["ClassicTick"]["MOONSHINE"] = 3;
        cards_number_map_orig_differentMode["ClassicTick"]["STICKEMUP"] = 3;
        cards_number_map_orig_differentMode["ClassicTick"]["GOLDSTASH"] = 4;
        cards_number_map_orig_differentMode["ClassicTick"]["BANK"] = 4;  //TODO make sure consistent numbers in pool

        cards_number_map_orig_differentMode["LawlessTick"] = new Dictionary<string, int>();
        cards_number_map_orig_differentMode["LawlessTick"]["GOLDBAR"] = 30;
        cards_number_map_orig_differentMode["LawlessTick"]["DRAW"] = 3;
        cards_number_map_orig_differentMode["LawlessTick"]["LASSO"] = 3;
        cards_number_map_orig_differentMode["LawlessTick"]["SHERIFF"] = 0;
        cards_number_map_orig_differentMode["LawlessTick"]["MOONSHINE"] = 3;
        cards_number_map_orig_differentMode["LawlessTick"]["STICKEMUP"] = 3;
        cards_number_map_orig_differentMode["LawlessTick"]["GOLDSTASH"] = 4;
        cards_number_map_orig_differentMode["LawlessTick"]["BANK"] = 4;  

        cards_number_map_orig_differentMode["DrinkingTick"] = new Dictionary<string, int>();
        cards_number_map_orig_differentMode["DrinkingTick"]["GOLDBAR"] = 30;
        cards_number_map_orig_differentMode["DrinkingTick"]["DRAW"] = 3;
        cards_number_map_orig_differentMode["DrinkingTick"]["LASSO"] = 3;
        cards_number_map_orig_differentMode["DrinkingTick"]["SHERIFF"] = 8;
        cards_number_map_orig_differentMode["DrinkingTick"]["MOONSHINE"] = 20;
        cards_number_map_orig_differentMode["DrinkingTick"]["STICKEMUP"] = 3;
        cards_number_map_orig_differentMode["DrinkingTick"]["GOLDSTASH"] = 4;
        cards_number_map_orig_differentMode["DrinkingTick"]["BANK"] = 4;  

        cards_number_map_orig_differentMode["WildWestTick"] = new Dictionary<string, int>();
        cards_number_map_orig_differentMode["WildWestTick"]["GOLDBAR"] = 0;
        cards_number_map_orig_differentMode["WildWestTick"]["DRAW"] = 3;
        cards_number_map_orig_differentMode["WildWestTick"]["LASSO"] = 3;
        cards_number_map_orig_differentMode["WildWestTick"]["SHERIFF"] = 8;
        cards_number_map_orig_differentMode["WildWestTick"]["MOONSHINE"] = 3;
        cards_number_map_orig_differentMode["WildWestTick"]["STICKEMUP"] = 3;
        cards_number_map_orig_differentMode["WildWestTick"]["GOLDSTASH"] = 4;
        cards_number_map_orig_differentMode["WildWestTick"]["BANK"] = 4;  
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown(){
        //  setnumber

       SliderUtil.setSliderValues(cards_number_map_orig_differentMode[this.gameObject.name]);
        //display tick
        GameObject tick = GameObject.Find("Tick");
        tick.transform.position = this.transform.position;

    }
}
