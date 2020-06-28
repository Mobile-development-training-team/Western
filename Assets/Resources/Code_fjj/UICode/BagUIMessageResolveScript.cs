using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUIMessageResolveScript : MonoBehaviour
{
    public void Click()
    {
        GameObject Buf =  transform.parent.parent.Find("Confirm").gameObject;
        if (BagUIMessageScript.pastIndex < DataManager.bag.GetItemBag().Count)
        {
            if (DataManager.bag.GetItemBag()[BagUIMessageScript.pastIndex].item.GetQuality() != Quality.Epic)
            {
                Buf.transform.Find("Background").Find("RareEarth").Find("Count").GetComponent<Text>().text = 'x' + DataManager.bag.GetItemBag()[BagUIMessageScript.pastIndex].GetResolveRareEarth().ToString();
            }
            else
            {
                return;
            }
        }
        else
        {
            switch (BagUIMessageScript.pastIndex - DataManager.bag.GetItemBag().Count)
            {
                case 0:
                    if (DataManager.roleEquipment.GetMainWeapon().item.GetQuality() != Quality.Epic)
                    {
                        Buf.transform.Find("Background").Find("RareEarth").Find("Count").GetComponent<Text>().text = 'x' + DataManager.roleEquipment.GetMainWeapon().GetResolveRareEarth().ToString();
                    }
                    else
                    {
                        return;
                    }
                    break;
                case 1:
                    if (DataManager.roleEquipment.GetAlternateWeapon().item.GetQuality() != Quality.Epic)
                    {
                        Buf.transform.Find("Background").Find("RareEarth").Find("Count").GetComponent<Text>().text = 'x' + DataManager.roleEquipment.GetAlternateWeapon().GetResolveRareEarth().ToString();
                    }
                    else
                    {
                        return;
                    }
                    break;
                case 2:
                    if (DataManager.roleEquipment.GetCuirass().item.GetQuality() != Quality.Epic)
                    {
                        Buf.transform.Find("Background").Find("RareEarth").Find("Count").GetComponent<Text>().text = 'x' + DataManager.roleEquipment.GetCuirass().GetResolveRareEarth().ToString();
                    }
                    break;
                case 3:
                    if (DataManager.roleEquipment.GetHelm().item.GetQuality() != Quality.Epic)
                    {
                        Buf.transform.Find("Background").Find("RareEarth").Find("Count").GetComponent<Text>().text = 'x' + DataManager.roleEquipment.GetHelm().GetResolveRareEarth().ToString();
                    }
                    else
                    {
                        return;
                    }
                    break;
            }
        }
        Buf.GetComponent<Canvas>().enabled = true;
    }
}
