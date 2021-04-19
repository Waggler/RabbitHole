using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutManager : MonoBehaviour
{

    public bool tut1check;
    public bool tut2Check;
    public bool tut3Check;

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;

    public GameObject pickup;

    public GameObject log1;
    public GameObject log2;
    public GameObject log3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DashButton"))
        {
            tut1check = true;
        }

        if (target1.activeSelf == false && target2.activeSelf == false && target3.activeSelf == false)
        {
            tut2Check = true;
        }

        if (pickup.activeSelf == false)
        {
            tut3Check = true;
        }

        if (tut1check == true && tut2Check == true && tut3Check == true)
        {
            log1.SetActive(false);
            log2.SetActive(false);
            log3.SetActive(false);
        }

    }
}
