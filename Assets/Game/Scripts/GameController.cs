using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform player_spawn_pos;
    public GameObject player_prefab;

    public GameObject player;


    public void OnMenu()
    {
        print("Menu");
        Camera cam = Camera.main;

        if (cam != null)
        {
            Destroy(cam.gameObject);
        }

        if (player != null)
        {
            Destroy(player);
        }

        player = Instantiate(player_prefab);
        player.transform.position = player_prefab.transform.position;

    }
}
