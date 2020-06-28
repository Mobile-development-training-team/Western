using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotteryMessageDoneButtonScript : MonoBehaviour
{
    public void Click()
    {
        transform.parent.GetComponent<Canvas>().enabled = false;
        transform.parent.parent.Find("Main").GetComponent<Canvas>().enabled = true;
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }
}
