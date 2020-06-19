using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LottertyMessageBackScript : MonoBehaviour
{
    public void Click()
    {
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }
}
