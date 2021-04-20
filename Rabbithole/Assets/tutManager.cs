using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutManager : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;

    public GameObject pickup;

    public GameObject log1;
    public GameObject log2;
    public GameObject log3;

    public GameObject pointer1;
    public GameObject pointer2;
    public GameObject pointer3;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.tutComplete == true)
        {
            target1.SetActive(false);
            target2.SetActive(false);
            target3.SetActive(false);
            pickup.SetActive(false);
            UnblockLevels();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DashButton"))
        {
            GameManager.Instance.tut1Check = true;
        }

        if (target1.activeSelf == false && target2.activeSelf == false && target3.activeSelf == false)
        {
            GameManager.Instance.tut2Check = true;
        }

        if (pickup.activeSelf == false)
        {
            GameManager.Instance.tut3Check = true;
        }

        if (GameManager.Instance.tutComplete == true)
        {
            UnblockLevels();
        }
    }

    void UnblockLevels()
    {
        log1.SetActive(false);
        log2.SetActive(false);
        log3.SetActive(false);

        pointer1.SetActive(true);
        pointer2.SetActive(true);
        pointer3.SetActive(true);
    }
}
