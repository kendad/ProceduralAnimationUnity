using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKScript : MonoBehaviour
{
    //PUBLIC VARIABLES
    public GameObject target;
    public bool isStatic=false;

    //PRIVATE VARIABELS
    private string baseBoneName = "Bone";
    private Vector3 basePosition;
    private float tolerenceVariable;
    private Vector3 startingPosition;

    private List<GameObject> bonesList = new List<GameObject>();
    private List<float> bonesDistanceList = new List<float>();
    private List<Vector3> bonePositions = new List<Vector3>();


    void Start()
    {
        bonesList.Add(this.gameObject);
        bool isBase = false;
        GameObject baseObject = this.gameObject;
        while (!isBase)
        {
            baseObject = baseObject.transform.parent.gameObject;
            if (baseObject.name.ToString() == baseBoneName)
            {
                isBase = true;
                basePosition = baseObject.transform.position;
            }
            bonesList.Add(baseObject);
        }

        if (baseObject != null)
        {
            startingPosition = baseObject.transform.position;
        }
        //margin of error
        tolerenceVariable = Vector3.Distance(target.transform.position, this.transform.position);
        //Distance between bones
        for(int i = 1; i < bonesList.Count; i++)
        {
            bonesDistanceList.Add(Vector3.Distance(bonesList[i].transform.position, bonesList[i - 1].transform.position));
        }
        //Previous positions
        for(int i = 0; i < bonesList.Count; i++)
        {
            bonePositions.Add(bonesList[i].transform.position);
        }
        backward();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isStatic = !isStatic;
        }
        if (isStatic == false)
        {
            backward();
        }
        else
        {
            basePosition = bonePositions[bonePositions.Count - 1];
            backward();
            forward();
        }
    }

    void forward()
    {
        bonePositions[bonesList.Count-1] = basePosition;
        for (int i = bonesList.Count-1; i < 0; i--)
        {
            float distance = bonesDistanceList[i];
            float magnitude = Mathf.Sqrt(Mathf.Pow(bonePositions[i].x - bonesList[i - 1].transform.position.x, 2) + Mathf.Pow(bonePositions[i].y - bonesList[i - 1].transform.position.y, 2) + Mathf.Pow(bonePositions[i].z - bonesList[i - 1].transform.position.z, 2));
            Vector3 direction = (bonesList[i - 1].transform.position - bonePositions[i]) / magnitude;
            Vector3 newPosition = direction * distance;
            newPosition += bonePositions[i];
            bonePositions[i - 1] = newPosition;
        }
        //Update Positions of bone
        for(int i = 0; i < bonesList.Count; i++)
        {
            bonesList[i].transform.position = bonePositions[i];
        }
    }

    void backward()
    {
        bonePositions[0] = target.transform.position;
        for (int i = 0; i < bonesList.Count-1; i++)
        {
            float distance = bonesDistanceList[i];
            float magnitude = Mathf.Sqrt(Mathf.Pow(bonePositions[i].x-bonesList[i+1].transform.position.x, 2)+ Mathf.Pow(bonePositions[i].y - bonesList[i + 1].transform.position.y, 2)+ Mathf.Pow(bonePositions[i].z - bonesList[i + 1].transform.position.z, 2));
            Vector3 direction = (bonesList[i + 1].transform.position- bonePositions[i]) /magnitude;
            Vector3 newPosition = direction * distance;
            newPosition += bonePositions[i];
            bonePositions[i + 1] = newPosition;
        }
        //update positions in reverse
        for (int i = bonesList.Count-1; i >= 0; i--)
        {
            bonesList[i].transform.position = bonePositions[i];
        }
    }
}
