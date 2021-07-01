using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangController : MonoBehaviour
{


    public void AddMember()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "member")
        {
            collision.transform.parent = transform.GetChild(0);
        }
    }



}
