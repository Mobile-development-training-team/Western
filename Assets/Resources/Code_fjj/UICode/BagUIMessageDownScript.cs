using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIMessageDownScript : MonoBehaviour
{
    public void Click()
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

                DataManager.bag.AddBagItem(DataManager.roleEquipment.GetMainWeapon());
                DataManager.roleEquipment.SetMainWeaponNull();
                break;

            case 1:
                GameScript.EquipmentModel[1].SetActive(false);
                GameScript.EquipmentModel[1] = null;
                if (GameScript.EquipmentModel[0] != null)
                {
                    GameScript.EquipmentModel[0].SetActive(true);
                }

                DataManager.bag.AddBagItem(DataManager.roleEquipment.GetAlternateWeapon());
                DataManager.roleEquipment.SetAlternateWeaponNull();
                break;

            case 2:
                GameScript.EquipmentModel[2].SetActive(false);
                GameScript.EquipmentModel[2] = null;

                DataManager.bag.AddBagItem(DataManager.roleEquipment.GetCuirass());
                DataManager.roleEquipment.SetCuirassNull();
                break;

            case 3:
                GameScript.EquipmentModel[3].SetActive(false);
                GameScript.EquipmentModel[3] = null;

                DataManager.bag.AddBagItem(DataManager.roleEquipment.GetHelm());
                DataManager.roleEquipment.SetHelmNull();
                break;
        }

        transform.parent.parent.Find("Cost").Find("RareEarth").gameObject.SetActive(false);

        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }
}
