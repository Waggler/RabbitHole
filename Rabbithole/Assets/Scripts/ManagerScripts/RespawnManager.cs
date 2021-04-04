using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public BoxCollider fallZone;
    public Vector3 respawnPoint;

    void Start()
    {
        respawnPoint = GameManager.Instance.player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {

            if (other.CompareTag("Player"))
            {
                if (GameManager.Instance.health > 0)
                {
                    GameManager.Instance.player.transform.position = respawnPoint;
                }
                else
                    //GameManager.Instance.Die();
                    return;

            }
            else
                return;
        }
}

