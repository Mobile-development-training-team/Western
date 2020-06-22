using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIListFieldTipScript : MonoBehaviour
{
    private BagItem ItemBuf;
    private BagItem DebrisBuf;
    private int RareEarthBuf;

    private void Start()
    {
        SelfUpdate();
    }

    private void Update()
    {
        if (ItemBuf.item.GetID() != DataManager.bag.GetItemBag()[transform.parent.parent.GetSiblingIndex()].item.GetID())
        {
            SelfUpdate();
            return;
        }
        int iBuf;
        iBuf = DataManager.bag.FindBagItem(DebrisBuf);
        if (iBuf > -1 && DebrisBuf.count != DataManager.bag.GetItemBag()[iBuf].count)
        {
            SelfUpdate();
            return;
        }
        if (RareEarthBuf != DataManager.roleEquipment.GetRareEarthCount())
        {
            SelfUpdate();
            return;
        }

    }

    public void SelfUpdate()
    {
        ItemBuf = DataManager.bag.GetItemBag()[transform.parent.parent.GetSiblingIndex()];
        RareEarthBuf = DataManager.roleEquipment.GetRareEarthCount();
        if (ItemBuf.item.GetEquipmentOrNot())
        {
            if (ItemBuf.level == 20)
            {
                GetComponent<Canvas>().enabled = false;
            }
            else
            {
                BagItem dBuf = new BagItem();
                dBuf.item = new Item();
                dBuf.item.FindItem(DataManager.GameItemIndex, ItemBuf.item.GetID() + 1);
                dBuf.level = 0;
                dBuf.count = 1;
                DebrisBuf = dBuf;
                int iBuf = DataManager.bag.FindBagItem(dBuf);
                if (iBuf > -1)
                {
                    if (DataManager.bag.GetItemBag()[iBuf].count >= ItemBuf.GetIntensifyDebris() && DataManager.roleEquipment.GetRareEarthCount() >= ItemBuf.GetIntensifyRareEarth())
                    {
                        GetComponent<Canvas>().enabled = true;
                    }
                    else
                    {
                        GetComponent<Canvas>().enabled = false;
                    }
                }
                else
                {
                    GetComponent<Canvas>().enabled = false;
                }
            }
        }
        else
        {
            DebrisBuf = ItemBuf;
            if (ItemBuf.count >= 15)
            {
                GetComponent<Canvas>().enabled = true;
            }
            else
            {
                GetComponent<Canvas>().enabled = false;
            }
        }
    }
}
