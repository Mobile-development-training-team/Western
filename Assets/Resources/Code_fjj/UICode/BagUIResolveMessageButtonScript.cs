using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIResolveMessageButtonScript : MonoBehaviour
{
    public void Click()
    {
        int Buf;
        if (BagUIMessageScript.pastIndex < DataManager.bag.GetItemBag().Count)
        {
            Buf = DataManager.bag.GetItemBag()[BagUIMessageScript.pastIndex].GetResolveRareEarth();
            DataManager.bag.ReduceItem(DataManager.bag.GetItemBag()[BagUIMessageScript.pastIndex]);
        }
        else
        {
            switch (BagUIMessageScript.pastIndex - DataManager.bag.GetItemBag().Count)
            {
                case 0:
                    Buf = DataManager.roleEquipment.GetMainWeapon().GetResolveRareEarth();
                    DataManager.roleEquipment.SetMainWeaponNull();
                    break;
                case 1:
                    Buf = DataManager.roleEquipment.GetAlternateWeapon().GetResolveRareEarth();
                    DataManager.roleEquipment.SetAlternateWeaponNull();
                    break;
                case 2:
                    Buf = DataManager.roleEquipment.GetCuirass().GetResolveRareEarth();
                    DataManager.roleEquipment.SetCuirassNull();
                    break;
                case 3:
                    Buf = DataManager.roleEquipment.GetHelm().GetResolveRareEarth();
                    DataManager.roleEquipment.SetHelmNull();
                    break;
                default:
                    Buf = 0;
                    break;
            }   
        }
        DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + Buf);
        transform.parent.GetComponent<Canvas>().enabled = false;
    }
}
