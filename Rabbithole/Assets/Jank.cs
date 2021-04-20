using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.player.transform.position.y <= -108.01f)
        {
            GameManager.Instance.player.transform.position = new Vector3(-154f, -42.63f, -1948f);
        }
    }
}
