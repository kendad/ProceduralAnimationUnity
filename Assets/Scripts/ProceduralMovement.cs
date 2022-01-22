using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMovement : MonoBehaviour
{
    //PUBLIC VARIABLES
    public GameObject feetTarget;
    public bool isActive;
    public RaycastHit hitTarget;
    //PRIVATE VARIBALES
    private Vector3 kneeInitialPosition;
    private GameObject creature;
    // Start is called before the first frame update
    void Start()
    {
        kneeInitialPosition = this.transform.position;
        creature = this.transform.parent.gameObject.transform.parent.gameObject;
        if(this.transform.parent.gameObject.name=="Leg1" || this.transform.parent.gameObject.name == "Leg4")
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit kneeHitTarget;
        RaycastHit feetHitTarget;
        bool kneeHit = Physics.Raycast(this.transform.position, this.transform.up, out kneeHitTarget, 10);
        bool feetHit = Physics.Raycast(feetTarget.transform.position, feetTarget.transform.right, out feetHitTarget, 1);
        Debug.DrawRay(this.transform.position, this.transform.up * kneeHitTarget.distance, Color.yellow);
        hitTarget = kneeHitTarget;

        if (isActive)
        {
            //FORWARD/BACKWARD MOVEMENT
            if (Mathf.Abs(Mathf.Abs(this.transform.position.x) - Mathf.Abs(kneeInitialPosition.x)) > 0.3)
            {
                kneeInitialPosition = this.transform.position;
                feetTarget.transform.position = kneeHitTarget.point;
            }
            //SIDEWAYS MOVEMENT
            if (Mathf.Abs(Mathf.Abs(this.transform.position.z) - Mathf.Abs(kneeInitialPosition.z)) > 0.3)
            {
                kneeInitialPosition = this.transform.position;
                feetTarget.transform.position = kneeHitTarget.point;
            }
        }
        isActive = !isActive;
    }
}
