using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public Camera cam;
    public float x_boundry_min = -7;
    public float x_boundry_max = 7;
    public float forward_speed = 2;

    public float smooth = 5;

    Vector3 intersect_0;
    Vector3 pos_0;
    Vector3 next_pos;
    Vector3 mouse_pos_0;

    Bounds bounds;

    private void Start()
    {
        next_pos = transform.position;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            intersect_0 = GetIntersectPoint();
            pos_0 = transform.position;
            mouse_pos_0 = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if (mouse_pos_0 - Input.mousePosition != Vector3.zero)
            {

                Vector3 delta = GetIntersectPoint() - intersect_0;
                Vector3 new_pos = pos_0 + delta;

                Bounds bounds = CalculateBounds();

                float size1 = Mathf.Abs(bounds.min.x - transform.position.x);
                float size2 = Mathf.Abs(bounds.max.x - transform.position.x);

                float clamped_x = Mathf.Clamp(new_pos.x, x_boundry_min + size1, x_boundry_max - size2);
                new_pos = new Vector3(clamped_x, new_pos.y, new_pos.z);

                if (transform.position != new_pos)
                {
                    next_pos = new_pos;
                }
                else
                {
                    intersect_0 = GetIntersectPoint();
                    pos_0 = next_pos;
                }
            }

            mouse_pos_0 = Input.mousePosition;
        }

        float x_lerp = Mathf.Lerp(transform.position.x, next_pos.x, Time.deltaTime * smooth);
        float z_lerp = transform.parent.position.z + Time.deltaTime * forward_speed;
        transform.parent.position = new Vector3(0, transform.parent.position.y, z_lerp);
        transform.localPosition = new Vector3(x_lerp, 0, 0);
    }

    public Vector3 GetIntersectPoint()
    {
        
        float y_screen_point_offset = cam.WorldToScreenPoint(transform.position).y;
        Ray cam_ray = cam.ScreenPointToRay(new Vector2(Input.mousePosition.x, y_screen_point_offset));

        Vector3 line1_point = cam_ray.origin;
        Vector3 line2_point = new Vector3(line1_point.x, 0, line1_point.z);

        Vector3 line1_dir = cam_ray.direction;
        Vector3 line2_dir = new Vector3(cam_ray.direction.x, 0, cam_ray.direction.z);

        //Debug.DrawRay(line1_point, line1_dir * 100, Color.black);
        //Debug.DrawRay(line2_point, line2_dir * 100, Color.red);

        Math3d.LineLineIntersection(out Vector3 intersection, line1_point, line1_dir, line2_point, line2_dir);

        return intersection;

    }

    public Bounds CalculateBounds()
    {
        Transform _objects = transform.GetChild(0);
        bounds = _objects.GetChild(0).GetComponent<Renderer>().bounds;

        foreach (Transform child in _objects)
        {
            bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
        }

 
        _objects.parent = null;
        transform.position = new Vector3(bounds.center.x, transform.position.y, bounds.center.z);
        _objects.parent = transform;

        return bounds;

    }



}
