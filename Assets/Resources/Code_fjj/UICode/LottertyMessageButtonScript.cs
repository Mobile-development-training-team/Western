using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LottertyMessageButtonScript : MonoBehaviour
{
    public void Click()
    {
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }
}
