using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIMessageMakeScript : MonoBehaviour
{
    public void Click()
    {
        BagItem Buf = DataManager.bag.GetItemBag()[BagUIMessageScript.pastIndex];
        if (Buf.count < 15)
        {
        }
        else
        {
            DataManager.bag.ReduceItem(Buf, 15);
            DataManager.bag.AddItem(new Item(DataManager.GameItemIndex, Buf.item.GetID() - 1));
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
}
