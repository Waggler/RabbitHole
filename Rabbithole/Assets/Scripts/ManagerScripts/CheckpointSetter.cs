using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSetter : MonoBehaviour
{
    public RespawnManager respawnManager;
    public Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            {
            respawnManager.respawnPoint = respawnPoint.transform.position;
            this.gameObject.SetActive(false);
            }
                else
                    //GameManager.Instance.Die();
                    return;
            }
    }
