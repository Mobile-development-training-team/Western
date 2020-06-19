using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUIMessageUpScript : MonoBehaviour
{
    public void Click()
    {
        BagItem Buf = DataManager.bag.GetItemBag()[BagUIMessageScript.pastIndex];
        switch (Buf.item.GetEquipmentType())
        {
            case EquipmentType.MainWeapon:
                if (DataManager.roleEquipment.HadMainWeapon())
                {
                    GameScript.EquipmentModel[0].SetActive(false);
                    GameScript.EquipmentModel[0] = GameScript.MainWeaponModelIndex[(int)Buf.item.GetQuality()];
                    GameScript.EquipmentModel[0].SetActive(true);
                    if (GameScript.EquipmentModel[1] != null)
                    {
                        GameScript.EquipmentModel[1].SetActive(false);
                    }

                    BagItem eBuf = new BagItem();
                    eBuf.item = new Item();
                    eBuf.item = DataManager.roleEquipment.GetMainWeapon().item;
                    eBuf.level = DataManager.roleEquipment.GetMainWeapon().level;
                    eBuf.count = DataManager.roleEquipment.GetMainWeapon().count;
                    DataManager.roleEquipment.SetMainWeapon(Buf);
                    DataManager.bag.ChangeBagItem(eBuf, BagUIMessageScript.pastIndex);
                }
                else
                {
                    GameScript.EquipmentModel[0] = GameScript.MainWeaponModelIndex[(int)Buf.item.GetQuality()];
                    GameScript.EquipmentModel[0].SetActive(true);
                    if (GameScript.EquipmentModel[1] != null)
                    {
                        GameScript.EquipmentModel[1].SetActive(false);
                    }

                    BagItem eBuf = new BagItem();
                    eBuf.item = new Item();
                    eBuf.item = Buf.item;
                    eBuf.level = Buf.level;
                    eBuf.count = Buf.count;
                    DataManager.roleEquipment.SetMainWeapon(eBuf);
                    DataManager.bag.ReduceItem(Buf);
                }
                break;
            case EquipmentType.AlternateWeapon:
                if (DataManager.roleEquipment.HadAlternateWeapon())
                {
                    GameScript.EquipmentModel[1].SetActive(false);
                    GameScript.EquipmentModel[1] = GameScript.AlternateWeaponModelIndex[(int)Buf.item.GetQuality()];
                    GameScript.EquipmentModel[1].SetActive(true);
                    if (GameScript.EquipmentModel[0] != null)
                    {
                        GameScript.EquipmentModel[0].SetActive(false);
                    }

                    BagItem eBuf = new BagItem();
                    eBuf.item = new Item();
                    eBuf.item = DataManager.roleEquipment.GetAlternateWeapon().item;
                    eBuf.level = DataManager.roleEquipment.GetAlternateWeapon().level;
                    eBuf.count = DataManager.roleEquipment.GetAlternateWeapon().count;
                    DataManager.roleEquipment.SetAlternateWeapon(Buf);
                    DataManager.bag.ChangeBagItem(eBuf, BagUIMessageScript.pastIndex);
                }
                else
                {
                    GameScript.EquipmentModel[1] = GameScript.AlternateWeaponModelIndex[(int)Buf.item.GetQuality()];
                    GameScript.EquipmentModel[1].SetActive(true);
                    if (GameScript.EquipmentModel[0] != null)
                    {
                        GameScript.EquipmentModel[0].SetActive(false);
                    }

                    BagItem eBuf = new BagItem();
                    eBuf.item = new Item();
                    eBuf.item = Buf.item;
                    eBuf.level = Buf.level;
                    eBuf.count = Buf.count;
                    DataManager.roleEquipment.SetAlternateWeapon(eBuf);
                    DataManager.bag.ReduceItem(Buf);
                }
                break;
            case EquipmentType.Cuirass:
                if (DataManager.roleEquipment.HadCuirass())
                {
                    GameScript.EquipmentModel[2].SetActive(false);
                    GameScript.EquipmentModel[2] = GameScript.CuirassModelIndex[(int)Buf.item.GetQuality()];
                    GameScript.EquipmentModel[2].SetActive(true);

                    BagItem eBuf = new BagItem();
                    eBuf.item = new Item();
                    eBuf.item = DataManager.roleEquipment.GetCuirass().item;
                    eBuf.level = DataManager.roleEquipment.GetCuirass().level;
                    eBuf.count = DataManager.roleEquipment.GetCuirass().count;
                    DataManager.roleEquipment.SetCuirass(Buf);
                    DataManager.bag.ChangeBagItem(eBuf, BagUIMessageScript.pastIndex);
                }
                else
                {
                    GameScript.EquipmentModel[2] = GameScript.CuirassModelIndex[(int)Buf.item.GetQuality()];
                    GameScript.EquipmentModel[2].SetActive(true);

                    BagItem eBuf = new BagItem();
                    eBuf.item = new Item();
                    eBuf.item = Buf.item;
                    eBuf.level = Buf.level;
                    eBuf.count = Buf.count;
                    DataManager.roleEquipment.SetCuirass(eBuf);
                    DataManager.bag.ReduceItem(Buf);
                }
                break;
            case EquipmentType.Helm:
                if (DataManager.roleEquipment.HadHelm())
                {
                    GameScript.EquipmentModel[3].SetActive(false);
                    GameScript.EquipmentModel[3] = GameScript.HelmModelIndex[(int)Buf.item.GetQuality()];
                    GameScript.EquipmentModel[3].SetActive(true);

                    BagItem eBuf = new BagItem();
                    eBuf.item = new Item();
                    eBuf.item = DataManager.roleEquipment.GetHelm().item;
                    eBuf.level = DataManager.roleEquipment.GetHelm().level;
                    eBuf.count = DataManager.roleEquipment.GetHelm().count;
                    DataManager.roleEquipment.SetHelm(Buf);
                    DataManager.bag.ChangeBagItem(eBuf, BagUIMessageScript.pastIndex);
                }
                else
                {
                    GameScript.EquipmentModel[3] = GameScript.HelmModelIndex[(int)Buf.item.GetQuality()];
                    GameScript.EquipmentModel[3].SetActive(true);

                    BagItem eBuf = new BagItem();
                    eBuf.item = new Item();
                    eBuf.item = Buf.item;
                    eBuf.level = Buf.level;
                    eBuf.count = Buf.count;
                    DataManager.roleEquipment.SetHelm(eBuf);
                    DataManager.bag.ReduceItem(Buf);
                }
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
