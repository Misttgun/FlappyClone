using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Camera offset on the X axis
    public static float offsetX;

    void Start()
    {

    }

    void Update()
    {
        if(BirdScript.instance != null && BirdScript.instance.isAlive)
        {
            MoveTheCamera();
        }
    }

    void MoveTheCamera()
    {
        Vector3 temp = transform.position;
        temp.x = BirdScript.instance.GetPositionX() + offsetX;
        transform.position = temp;
    }
}
