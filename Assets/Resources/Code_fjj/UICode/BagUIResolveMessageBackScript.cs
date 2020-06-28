using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIResolveMessageBackScript : MonoBehaviour
{
    public void Click()
    {
        transform.parent.GetComponent<Canvas>().enabled = false;
    }
}
