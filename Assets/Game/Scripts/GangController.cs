using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

public class GangController : MonoBehaviour
{
    public Transform radar;
    public Transform members;
    float no_members = 0;
    Bounds bounds;

    public GameEventListener on_die;

    bool play = false;

    private void Update()
    {
        if (!play)
        {
            return;
        }

        if (members.childCount == 0)
        {
            on_die.Event.Invoke("");
            play = false;

            GameEventMessage.SendEvent("OnDie");

            Destroy(transform.parent.gameObject);
        }

        if (no_members != members.childCount){
            CalculateBounds();
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "member")
        {
            collision.transform.parent = members;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.tag = "Untagged";
            Transform m =  members.GetChild(0);
            m.parent = null;
            Vector3 new_pos = other.ClosestPointOnBounds(m.position);
            m.position = new Vector3(new_pos.x, m.position.y, new_pos.z);
            m.LookAt(other.transform);
            m.tag = "Untagged";
        }
    }

    public Bounds CalculateBounds()
    {
        if (no_members != members.childCount && members.childCount > 0)
        {
            bounds = members.GetChild(0).GetComponent<Renderer>().bounds;

            foreach (Transform child in members)
            {
                bounds.Encapsulate(child.GetComponent<Renderer>().bounds);
            }
            radar.localScale = new Vector3(bounds.extents.x*2,2, bounds.extents.z * 2) + Vector3.forward * 5 + Vector3.right * 6;
            radar.localPosition = new Vector3(0, 0, 6/2);
        }

        bounds.center = transform.position;

        return bounds;

    }


    public void OnPlay()
    {
        play = true;
    }
}
