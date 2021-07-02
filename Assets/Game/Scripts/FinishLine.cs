using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    public GameObject point_prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "radar")
        {
            Transform members = other.transform.Find("members_ref").GetComponent<TransformValue>().value;
            Transform player = other.transform.Find("player_ref").GetComponent<TransformValue>().value;
            Transform main_cam = other.transform.Find("main_cam").GetComponent<TransformValue>().value;

            members.parent = transform;
            main_cam.parent = transform;
            Destroy(player.gameObject);

            BonusPoints bp = members.gameObject.AddComponent<BonusPoints>();
            bp.members = members;
            bp.cam = main_cam;
            bp.finish_line = transform;
            bp.point_prefab = point_prefab;

        }
    }
}
