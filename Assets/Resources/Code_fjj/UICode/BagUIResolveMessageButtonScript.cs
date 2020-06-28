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
                    GameScript.EquipmentModel[0].SetActive(false);
                    GameScript.EquipmentModel[0] = null;
                    if (GameScript.EquipmentModel[1] != null)
                    {
                        GameScript.EquipmentModel[1].SetActive(true);
                    }

                    Buf = DataManager.roleEquipment.GetMainWeapon().GetResolveRareEarth();
                    DataManager.roleEquipment.SetMainWeaponNull();
                    break;
                case 1:
                    GameScript.EquipmentModel[1].SetActive(false);
                    GameScript.EquipmentModel[1] = null;
                    if (GameScript.EquipmentModel[0] != null)
                    {
                        GameScript.EquipmentModel[0].SetActive(true);
                    }


                    Buf = DataManager.roleEquipment.GetAlternateWeapon().GetResolveRareEarth();
                    DataManager.roleEquipment.SetAlternateWeaponNull();
                    break;
                case 2:
                    GameScript.EquipmentModel[2].SetActive(false);
                    GameScript.EquipmentModel[2] = null;
                    Buf = DataManager.roleEquipment.GetCuirass().GetResolveRareEarth();
                    DataManager.roleEquipment.SetCuirassNull();
                    break;
                case 3:
                    GameScript.EquipmentModel[3].SetActive(false);
                    GameScript.EquipmentModel[3] = null;
                    Buf = DataManager.roleEquipment.GetHelm().GetResolveRareEarth();
                    DataManager.roleEquipment.SetHelmNull();
                    break;
                default:
                    Buf = 0;
                    break;
            }   
        }
        DataManager.roleEquipment.SetRareEarthCount(DataManager.roleEquipment.GetRareEarthCount() + Buf);

        transform.parent.parent.GetComponent<Canvas>().enabled = false;
        transform.parent.parent.parent.GetComponent<Canvas>().enabled = false;
    }
}
