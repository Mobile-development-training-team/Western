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
                DataManager.bag.AddBagItem(DataManager.roleEquipment.GetMainWeapon());
                DataManager.roleEquipment.SetMainWeaponNull();
                break;
            case 1:
                DataManager.bag.AddBagItem(DataManager.roleEquipment.GetAlternateWeapon());
                DataManager.roleEquipment.SetAlternateWeaponNull();
                break;
            case 2:
                DataManager.bag.AddBagItem(DataManager.roleEquipment.GetCuirass());
                DataManager.roleEquipment.SetCuirassNull();
                break;
            case 3:
                DataManager.bag.AddBagItem(DataManager.roleEquipment.GetHelm());
                DataManager.roleEquipment.SetHelmNull();
                break;
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
