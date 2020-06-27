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
            transform.parent.parent.Find("Tip").GetComponent<Canvas>().enabled = true;
            Invoke("TipDisable",1);
        }
        else
        {
            DataManager.bag.ReduceItem(Buf, 15);
            DataManager.bag.AddItem(new Item(DataManager.GameItemIndex, Buf.item.GetID() - 1));

            transform.parent.parent.Find("Cost").Find("RareEarth").gameObject.SetActive(false);

            transform.parent.parent.GetComponent<Canvas>().enabled = false;
        }
    }

    public void TipDisable()
    {
        transform.parent.parent.Find("Tip").GetComponent<Canvas>().enabled = false;
    }
}
