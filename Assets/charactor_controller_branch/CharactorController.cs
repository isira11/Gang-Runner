using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    public float x_boundry_min = -7;
    public float x_boundry_max = 7;

    Vector3 intersect_0;
    Vector3 pos_0;



    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            intersect_0 = GetIntersectPoint();
            pos_0 = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = GetIntersectPoint() - intersect_0;
            Vector3 new_pos = pos_0 + delta;

            Bounds bounds = CalculateBounds();

            float size1 = Mathf.Abs(bounds.min.x - transform.position.x);
            float size2 = Mathf.Abs(bounds.max.x - transform.position.x);

            float clamped_x =  Mathf.Clamp(new_pos.x, x_boundry_min + size1, x_boundry_max - size2);
            new_pos = new Vector3(clamped_x, new_pos.y, new_pos.z);

            if (transform.position != new_pos)
            {
                transform.position = new_pos;
            }
            else
            {
                intersect_0 = GetIntersectPoint();
                pos_0 = transform.position;
            }
        }
    }

    public Vector3 GetIntersectPoint()
    {
        float y_screen_point_offset = Camera.main.WorldToScreenPoint(transform.position).y;
        Ray cam_ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, y_screen_point_offset));

        Vector3 line1_point = cam_ray.origin;
        Vector3 line2_point = new Vector3(line1_point.x, 0, line1_point.z);

        Vector3 line1_dir = cam_ray.direction;
        Vector3 line2_dir = new Vector3(cam_ray.direction.x, 0, cam_ray.direction.z);

        Debug.DrawRay(line1_point, line1_dir * 100, Color.black);
        Debug.DrawRay(line2_point, line2_dir * 100, Color.red);
        Math3d.LineLineIntersection(out Vector3 intersection, line1_point, line1_dir, line2_point, line2_dir);

        return intersection;

    }

    public Bounds CalculateBounds()
    {
        Bounds bounds = gameObject.GetComponent<Renderer>().bounds;
        foreach (Transform child in gameObject.transform)
        {
            bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
        }

        return bounds;

    }



}
