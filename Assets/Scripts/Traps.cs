using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Traps : MonoBehaviour
{
     PlayerLivesManager playerLivesManager;
     void Start()
     {

     }

     void OnTriggerEnter2D(Collider2D collision)
     {
          GameObject player = GameObject.FindWithTag("Player");
          if (player != null && collision.CompareTag("Player"))
          {
               playerLivesManager = player.GetComponent<PlayerLivesManager>();
               if (collision.gameObject.tag == "Player")
               {
                    playerLivesManager.ReduceLives();
               }
          }
     }


}

