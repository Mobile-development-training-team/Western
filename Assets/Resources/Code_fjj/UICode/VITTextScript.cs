using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VITTextScript : MonoBehaviour
{
    private int Buf;
    void Start()
    {
        SelfUpdate();
    }

    private void SelfUpdate()
    {
        Buf = GameScript.VIT;
        GetComponent<Text>().text = Buf.ToString() + " / " + 24;
    }

    void Update()
    {
        if (Buf != GameScript.VIT)
        {
            SelfUpdate();
        }
    }
}
