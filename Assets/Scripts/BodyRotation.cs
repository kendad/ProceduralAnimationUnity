using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    //PRIVATE VARIBALE
    private GameObject Leg1;
    private GameObject Leg2;
    private GameObject Leg3;
    private GameObject Leg4;
    private float rightDist;
    private float leftDist;
    // Start is called before the first frame update
    void Start()
    {
        Leg1 = this.transform.Find("Leg1").gameObject;//LeftFront
        Leg2 = this.transform.Find("Leg2").gameObject;//LeftBack
        Leg3 = this.transform.Find("Leg3").gameObject;//RightFront
        Leg4 = this.transform.Find("Leg4").gameObject;//LeftFront
    }

    // Update is called once per frame
    void Update()
    {
        calculateAverage();
    }

    void calculateAverage()
    {
        rightDist = (Leg3.transform.Find("KneeTarget").GetComponent<ProceduralMovement>().hitTarget.distance+ Leg4.transform.Find("KneeTarget").GetComponent<ProceduralMovement>().hitTarget.distance)/2;
        leftDist = (Leg1.transform.Find("KneeTarget").GetComponent<ProceduralMovement>().hitTarget.distance+ Leg2.transform.Find("KneeTarget").GetComponent<ProceduralMovement>().hitTarget.distance)/2;
        Debug.Log("RightDist: "+rightDist);
        Debug.Log("LeftDist: "+leftDist);
        if (Mathf.Abs(rightDist - leftDist) > 0.1)
        {
            if (rightDist > leftDist)
            {
                this.transform.rotation=Quaternion.Slerp(this.transform.rotation,Quaternion.Euler(10,0,0),0.5f);
            }
            else
            {
                this.transform.rotation=Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(-10, 0, 0), 0.5f);
            }
        }
    }
}
