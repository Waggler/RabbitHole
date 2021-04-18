using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BeachManager : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public GameObject goonie;
    public Transform[] spawnPoints = new Transform[3];

    [Header("Spawn Wait Times")]
    public float spawnWait;
    public float startWait;
    public float waveWait;

    [Header("Enemy Spawn Tracking")]
    public int maxEnemyCount = 4;
    public int rawEnemyCount;
    public int currentEnemies;

    [Header("Timer Settings")]
    public float timer = 80f;
    public float timeRemaining;
    [SerializeField] TextMeshProUGUI timerText;

    [Header("Pickup Spawning")]
    public GameObject HCarrot;
    public GameObject timePickup;
    public float HCarrotTimer;
    public int timePickupKC;
    public Transform timePoint;
    public Transform carrotPoint;

    void Start()
    {
        GameManager.Instance.enemiesKilled = 0;
        GameManager.Instance.timeReduced = 0;
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        //enemy counter tracker bandaid
        currentEnemies = rawEnemyCount - GameManager.Instance.enemiesKilled;

        //reduces timer and sends it to UI & bandaid time pickup
        timer = timer - 1 * Time.deltaTime;
        timeRemaining = timer - GameManager.Instance.timeReduced;
        
        if(timeRemaining <= 0)
        {
            timeRemaining = 0;
        }
        timerText.text = timeRemaining.ToString("0");

        //Time Pickup Spawner
        if (GameManager.Instance.enemiesKilled % 4 == 0 && GameManager.Instance.enemiesKilled > 0)
        {

            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(timePickup, timePoint.position, spawnRotation);
        }

        //Health Pickup Spawner
        if(timeRemaining % 10 == 0 && timeRemaining > 0)
        {
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(HCarrot, carrotPoint.position, spawnRotation);
        }
    }
    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startWait);
        while (GameManager.Instance.isPaused == false && currentEnemies < maxEnemyCount && timeRemaining > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (Transform spawnPoint in spawnPoints)
                {
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(goonie, spawnPoint.position, spawnRotation);
                    rawEnemyCount++;
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);
            }
        }
    }
}
