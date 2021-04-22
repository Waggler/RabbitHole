using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
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

    public GameObject intro1;
    public GameObject intro2;
    public GameObject intro3;

    public BoxCollider jacklynIntroTrigger;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.tutComplete == true)
        {
            jacklynIntroTrigger.enabled = false;
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
        if(GameManager.Instance.level1Complete == false)
        {
            log1.SetActive(false);
            pointer1.SetActive(true);
            intro1.SetActive(true);
        }
        
        if(GameManager.Instance.level2Complete == false)
        {
            log2.SetActive(false);
            pointer2.SetActive(true);
            intro2.SetActive(true);
        }
        
        if(GameManager.Instance.level3Complete == false)
        {
            log3.SetActive(false);
            pointer3.SetActive(true);
            intro3.SetActive(true);
        }
    }
}
