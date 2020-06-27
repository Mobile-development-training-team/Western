using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIMessageIntensifyScript : MonoBehaviour
{
    public void Click()
    {
        if (BagUIMessageScript.pastIndex < DataManager.bag.GetItemBag().Count)
        {
            BagItem Buf = DataManager.bag.GetItemBag()[BagUIMessageScript.pastIndex];
            BagItem dBuf = new BagItem();
            dBuf.item = new Item();
            dBuf.item.FindItem(DataManager.GameItemIndex, Buf.item.GetID() + 1);
            dBuf.level = 0;
            dBuf.count = 1;
            int iBuf = DataManager.bag.FindBagItem(dBuf);
            if (iBuf > -1)
            {
                if (DataManager.bag.GetItemBag()[iBuf].count >= Buf.GetIntensifyDebris() && DataManager.roleEquipment.GetRareEarthCount() >= Buf.GetIntensifyRareEarth())
                {
                    DataManager.bag.ReduceItem(dBuf, Buf.GetIntensifyDebris());
                    DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() - Buf.GetIntensifyRareEarth());
                    DataManager.bag.BagItemLevelUp(BagUIMessageScript.pastIndex);
                }
                else
                {
                    transform.parent.parent.Find("Tip").GetComponent<Canvas>().enabled = true;
                    Invoke("TipDisable", 1);
                    return;
                }
            }
            else
            {
                transform.parent.parent.Find("Tip").GetComponent<Canvas>().enabled = true;
                Invoke("TipDisable", 1);
                return;
            }
        }
        else
        {
            BagItem Buf;
            switch (BagUIMessageScript.pastIndex - DataManager.bag.GetItemBag().Count)
            {
                case 0:
                    Buf = DataManager.roleEquipment.GetMainWeapon();
                    break;
                case 1:
                    Buf = DataManager.roleEquipment.GetAlternateWeapon();
                    break;
                case 2:
                    Buf = DataManager.roleEquipment.GetCuirass();
                    break;
                case 3:
                    Buf = DataManager.roleEquipment.GetHelm();
                    break;
                default:
                    return;
            }
            BagItem dBuf = new BagItem();
            dBuf.item = new Item();
            dBuf.item.FindItem(DataManager.GameItemIndex, Buf.item.GetID() + 1);
            dBuf.level = 0;
            dBuf.count = 1;
            int iBuf = DataManager.bag.FindBagItem(dBuf);
            if (iBuf > -1)
            {
                if (DataManager.bag.GetItemBag()[iBuf].count >= Buf.GetIntensifyDebris() && DataManager.roleEquipment.GetRareEarthCount() >= Buf.GetIntensifyRareEarth())
                {
                    int cBuf = DataManager.bag.GetItemBag().Count;
                    DataManager.bag.ReduceItem(dBuf, Buf.GetIntensifyDebris());
                    DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() - Buf.GetIntensifyRareEarth());
                    switch (BagUIMessageScript.pastIndex - cBuf)
                    {
                        case 0:
                            DataManager.roleEquipment.MainWeaponLevelUp();
                            break;
                        case 1:
                            DataManager.roleEquipment.AlternateWeaponLevelUp();
                            break;
                        case 2:
                            DataManager.roleEquipment.CuirassLevelUp();
                            break;
                        case 3:
                            DataManager.roleEquipment.HelmLevelUp();
                            break;
                    }
                }
                else
                {
                    transform.parent.parent.Find("Tip").GetComponent<Canvas>().enabled = true;
                    Invoke("TipDisable", 1);
                    return;
                }
            }
            else
            {
                transform.parent.parent.Find("Tip").GetComponent<Canvas>().enabled = true;
                Invoke("TipDisable", 1);
                return;
            }

        }
        transform.parent.parent.Find("Cost").Find("RareEarth").gameObject.SetActive(false);

        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }

    public void TipDisable()
    {
        transform.parent.parent.Find("Tip").GetComponent<Canvas>().enabled = false;
    }
}
