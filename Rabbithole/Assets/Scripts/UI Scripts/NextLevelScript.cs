using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public GameObject beachTrigger;
    public GameObject burrowTrigger;
    public GameObject mountainTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Burrow");
        }
    }



    public void Burrow()
    {
        SceneManager.LoadScene("Burrow");
        Debug.Log("Load Burrow level");
    }

    public void Mountain()
    {
        SceneManager.LoadScene("Mountain");
        Debug.Log("Load Mountain level");
    }

    public void Beach()
    {
        SceneManager.LoadScene("Beach");
        Debug.Log("Load Beach level");
    }
}
