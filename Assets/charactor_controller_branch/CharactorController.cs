using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{

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
            transform.position = pos_0 + delta;
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


}
