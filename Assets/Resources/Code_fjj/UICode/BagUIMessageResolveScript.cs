using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUIMessageResolveScript : MonoBehaviour
{
    public void Click()
    {
        transform.parent.Find("Material").Find("List").Find("Debris").gameObject.SetActive(false);
        transform.parent.Find("Material").Find("List").Find("RareEarth").gameObject.SetActive(false);
        transform.parent.Find("Make").gameObject.SetActive(false);
        transform.parent.Find("Up").gameObject.SetActive(false);
        transform.parent.Find("Down").gameObject.SetActive(false);
        transform.parent.Find("Resolve").gameObject.SetActive(false);
        transform.parent.Find("Intensify").gameObject.SetActive(false);
        transform.parent.GetComponent<Canvas>().enabled = false;

        GameObject Buf =  transform.parent.parent.Find("ResolveMessage").gameObject;
        if (BagUIMessageScript.pastIndex < DataManager.bag.GetItemBag().Count)
        {
            Buf.transform.Find("RareEarth").Find("Text").GetComponent<Text>().text = 'x' + DataManager.bag.GetItemBag()[BagUIMessageScript.pastIndex].GetResolveRareEarth().ToString();
        }
        else
        {
            switch (BagUIMessageScript.pastIndex - DataManager.bag.GetItemBag().Count)
            {
                case 0:
                    Buf.transform.Find("RareEarth").Find("Text").GetComponent<Text>().text = 'x' + DataManager.roleEquipment.GetMainWeapon().GetResolveRareEarth().ToString();
                    break;
                case 1:
                    Buf.transform.Find("RareEarth").Find("Text").GetComponent<Text>().text = 'x' + DataManager.roleEquipment.GetAlternateWeapon().GetResolveRareEarth().ToString();
                    break;
                case 2:
                    Buf.transform.Find("RareEarth").Find("Text").GetComponent<Text>().text = 'x' + DataManager.roleEquipment.GetCuirass().GetResolveRareEarth().ToString();
                    break;
                case 3:
                    Buf.transform.Find("RareEarth").Find("Text").GetComponent<Text>().text = 'x' + DataManager.roleEquipment.GetHelm().GetResolveRareEarth().ToString();
                    break;
            }
        }
        Buf.GetComponent<Canvas>().enabled = true;
    }
}
