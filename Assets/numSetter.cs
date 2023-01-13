using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class numSetter : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
        slider.onValueChanged.AddListener(delegate {ValueChange();});
         
    
    }

    public void ValueChange(){
        int max = 100;
        int min =0;
        int display_v = (int)(min+(slider.value)*(max-min));
        string num_obj = this.gameObject.name.Split("_")[0]+"_num";
        Debug.Log(num_obj);
        GameObject.Find(num_obj).GetComponent<TextMeshProUGUI>().text = display_v.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
