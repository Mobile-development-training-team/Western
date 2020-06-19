using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIMessageBackScript : MonoBehaviour
{
    public void Click()
    {
        transform.parent.Find("Material").Find("List").Find("Debris").gameObject.SetActive(false);
        transform.parent.Find("Material").Find("List").Find("RareEarth").gameObject.SetActive(false);
        transform.parent.Find("Make").gameObject.SetActive(false);
        transform.parent.Find("Up").gameObject.SetActive(false);
        transform.parent.Find("Down").gameObject.SetActive(false);
        transform.parent.Find("Resolve").gameObject.SetActive(false);
        transform.parent.Find("Intensify").gameObject.SetActive(false);
        transform.parent.GetComponent<Canvas>().enabled = false;
    }
}
