using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lets us enter a bunch of fields in inspector
[System.Serializable]
public class Dialogue
{

    public string name;

    [TextArea (1, 10)]
    public string[] sentences;


}
