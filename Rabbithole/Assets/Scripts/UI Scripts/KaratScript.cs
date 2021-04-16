using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KaratScript : MonoBehaviour
{

    [Header("Karats")]

    public GameObject karatOne;
    public GameObject karatTwo;
    public GameObject karatThree;

    [Header("Karat UI")]

    public GameObject karatOneUI;
    public GameObject karatTwoUI;
    public GameObject karatThreeUI;

    public int karatPieces;

    private void Start()
    {
        karatPieces = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        karatPieces += 1;

            if (karatPieces == 1)
            { 
                karatOneUI.gameObject.SetActive(true); 
            } else if (karatPieces == 2)
        {
                karatTwoUI.gameObject.SetActive(true);
        } else if (karatPieces == 3)
            if (karatPieces == 3)
            {
                karatThreeUI.gameObject.SetActive(true);
            }
    }
}
