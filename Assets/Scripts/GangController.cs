using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangController : MonoBehaviour
{
    public Transform radar;
    public Transform members;
    float no_members = 0;
    Bounds bounds;



    private void Update()
    {
        if (no_members != members.childCount){
            CalculateBounds();
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "member")
        {
            collision.transform.parent = members;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            Transform m =  members.GetChild(0);
            m.parent = null;
            Vector3 new_pos = other.ClosestPointOnBounds(m.position);
            m.position = new Vector3(new_pos.x, m.position.y, new_pos.z);
            m.LookAt(other.transform);
        }
    }

    public Bounds CalculateBounds()
    {
        if (no_members != members.childCount)
        {
            bounds = members.GetChild(0).GetComponent<Renderer>().bounds;

            foreach (Transform child in members)
            {
                bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
            }
            radar.localScale = new Vector3(bounds.extents.x*2,2, bounds.extents.z * 2) + Vector3.forward * 5 + Vector3.right * 6;
            radar.localPosition = new Vector3(0, 0, 6/2);
            print(bounds.extents);
        }

        bounds.center = transform.position;
        no_members = members.childCount;

        return bounds;

    }



}
