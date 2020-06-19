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
                    return;
                }
            }
            else
            {
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
                    return;
                }
            }
            else
            {
                return;
            }

        }

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
