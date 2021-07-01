using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targets;
    public float smooth;
    public Camera main;
    Camera cam;
    Vector3 new_pos;
    Vector3 target_pos_0;


    private void Start()
    {
        cam = GetComponent<Camera>();
        new_pos = transform.position;
        target_pos_0 = CalculateBounds().center;
    }

    private void Update()
    {
        Bounds bounds = CalculateBounds();
        Vector3 center = bounds.center;
        Vector3 x_min = new Vector3(bounds.min.x, center.y, center.z);
        Vector3 x_max = new Vector3(bounds.max.x, center.y, center.z);

        float min_to_screen = cam.WorldToScreenPoint(x_min).x;
        float max_to_screen = cam.WorldToScreenPoint(x_max).x;

        if (min_to_screen <= 0 || max_to_screen >= Screen.width)
        {
            Vector3 _new = transform.position - (target_pos_0 - bounds.center);
            new_pos = new Vector3(_new.x, transform.position.y, transform.position.z);
            transform.position = new_pos;
        }

        target_pos_0 = bounds.center;

        main.transform.localPosition = Vector3.Lerp(main.transform.localPosition,transform.localPosition, Time.deltaTime*smooth);
    }

    public Bounds CalculateBounds()
    {
        Transform _objects = targets.transform.GetChild(0);
        Bounds bounds = _objects.GetChild(0).GetComponent<Renderer>().bounds;
        foreach (Transform child in _objects)
        {
            bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
        }
        return bounds;

    }


}
