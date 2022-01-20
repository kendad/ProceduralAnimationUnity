using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.transform.position = this.transform.parent.gameObject.transform.position;
        this.transform.parent.gameObject.transform.position = this.transform.parent.gameObject.transform.position + new Vector3(0, 1, 0);
        this.transform.parent.gameObject.transform.parent.gameObject.transform.position = this.transform.parent.gameObject.transform.parent.gameObject.transform.position + new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
