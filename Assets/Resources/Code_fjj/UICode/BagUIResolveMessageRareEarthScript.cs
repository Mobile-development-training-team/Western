using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUIResolveMessageRareEarthScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().sprite = GameScript.QualityExcellent;
        transform.Find("Icon").GetComponent<Image>().sprite = GameScript.RareEarthSprite;
    }
}
