using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    
    public Vector2 mouse_pos = Vector2.zero;
    public float z_offset = 0;

    float y_screen_point_offset;


    void Update()
    {
        y_screen_point_offset = Camera.main.WorldToScreenPoint(new Vector3(0,0, z_offset)).y;

        if (Input.touchCount > 0)
        {
            Ray cam_ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, y_screen_point_offset));


            Vector3 line1_point = cam_ray.origin;
            Vector3 line2_point = new Vector3(line1_point.x, 0, line1_point.z);

            Vector3 line1_dir = cam_ray.direction;
            Vector3 line2_dir = new Vector3(cam_ray.direction.x, 0, cam_ray.direction.z);

            Debug.DrawRay(line1_point, line1_dir * 100, Color.black);
            Debug.DrawRay(line2_point, line2_dir * 100, Color.red);

			Vector3 intersection;

			if (Math3d.LineLineIntersection(out intersection, line1_point, line1_dir, line2_point, line2_dir))
            {
                transform.position = intersection;
                print(intersection);
                ///print(intersection);

			}



        }
    }

}
