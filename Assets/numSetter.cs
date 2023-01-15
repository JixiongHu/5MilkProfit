using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class numSetter : MonoBehaviour
{
    public Slider slider;
    public int max =100;
    public int min =0 ;
    // Start is called before the first frame update
    void Start()
    {
               max = 100;
      min =0;
        slider.onValueChanged.AddListener(delegate {ValueChange();});
         
    
    }

    public void ValueChange(){
        int display_v = (int)(min+(slider.value)*(max-min));
        string num_obj = this.gameObject.name.Split("_")[0]+"_num";
        Debug.Log(num_obj);
        GameObject.Find(num_obj).GetComponent<TextMeshProUGUI>().text = display_v.ToString();
        int totalNum = SliderUtil.getTotalNum();
        GameObject.Find("total_num").GetComponent<TextMeshProUGUI>().text =  totalNum.ToString();
        if( totalNum<16 ||totalNum>=100){
            GameObject.Find("total_num").GetComponent<TextMeshProUGUI>().fontSharedMaterial.SetColor("_FaceColor", new Color(0.8f,0.0f,0.0f,1.0f));

        }
        else{

            GameObject.Find("total_num").GetComponent<TextMeshProUGUI>().fontSharedMaterial.SetColor("_FaceColor", new Color(0.0f,0.0f,0.0f,1.0f));

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
