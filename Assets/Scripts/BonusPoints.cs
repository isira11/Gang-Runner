using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    public Transform members;
    public Transform cam;
    public GameObject point_prefab;
    public Transform finish_line;


    float speed = 20;

    private void Start()
    {
        gameObject.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        Vector3 start = finish_line.position + Vector3.forward * 15;

        for (int i = 0; i < members.childCount; i++)
        {
            GameObject point = Instantiate(point_prefab);
            point.transform.position = start + Vector3.forward * (i*15);
        }

    }

    private void Update()
    {
        members.position += Vector3.forward * Time.deltaTime * speed;
        cam.position += Vector3.forward * Time.deltaTime * speed;
        members.position = Vector3.Lerp(members.position, new Vector3(0, members.position.y, members.position.z), Time.deltaTime * 5);
        cam.position = new Vector3(members.position.x, cam.position.y, cam.position.z);

        if (members.childCount == 0) 
        {
            speed = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "point")
        {
            print(collision.transform.gameObject);
            collision.transform.tag = "Untagged";
            if (collision.contactCount > 0)
            {
                Transform  c = collision.GetContact(0).thisCollider.gameObject.transform;
                c.parent = null;

            }

        }



    }

}
