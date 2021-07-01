using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixer : MonoBehaviour
{
    public Camera main;
    public Camera[] others;


    void Start()
    {
        foreach (Camera item in others)
        {
            item.gameObject.transform.position = main.transform.position;
            item.gameObject.transform.rotation = main.transform.rotation;
            item.fieldOfView = main.fieldOfView;


        }
    }

}
