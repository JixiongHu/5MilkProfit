using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardposition : MonoBehaviour
{
    // Start is called before the first frame update
    bool updated=false;
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f,0.0f);
        this.transform.localScale= new Vector3(0.095f,0.095f, 0.095f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!updated){
            GameObject orig =GameObject.Find(this.gameObject.name.Split("_")[0]);
        this.transform.position = new Vector3(orig.transform.position.x, orig.transform.position.y, orig.transform.position.z-0.05f);}}
}
