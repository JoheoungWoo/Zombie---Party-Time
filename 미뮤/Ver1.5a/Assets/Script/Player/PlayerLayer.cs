using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("NPC"))
        {
            player.ChangeOrderByLayer(6);
        }
        /*
        if(collision.CompareTag("PlayerHitDecision"))
        {

        }
         */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("NPC"))
        {
            player.ChangeOrderByLayer(4);
        }
    }
}
