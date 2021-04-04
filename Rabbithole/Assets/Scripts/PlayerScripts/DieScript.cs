using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{

    public float lifeTime;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
