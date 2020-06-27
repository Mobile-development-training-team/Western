using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMessageConfirmBackScript : MonoBehaviour
{
    public void Click()
    {
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }
}
