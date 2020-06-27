using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagRoleEquipmentTipScript : MonoBehaviour
{
    private bool Had;
    private BagItem ItemBuf;
    private BagItem DebrisBuf;
    private int RareEarthBuf;

    private void Start()
    {
        SelfUpdate();
    }

    private void Update()
    {
        switch (transform.parent.parent.GetSiblingIndex())
        {
            case 0:
                if (Had != DataManager.roleEquipment.HadMainWeapon())
                {
                    SelfUpdate();
                    return;
                }
                else
                {
                    if (Had)
                    {
                        if (ItemBuf.item.GetID() != DataManager.roleEquipment.GetMainWeapon().item.GetID())
                        {
                            SelfUpdate();
                            return;
                        }
                        if (ItemBuf.level != DataManager.roleEquipment.GetMainWeapon().level)
                        {
                            SelfUpdate();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                break;
            case 1:
                if (Had != DataManager.roleEquipment.HadAlternateWeapon())
                {
                    SelfUpdate();
                    return;
                }
                else
                {
                    if (Had)
                    {
                        if (ItemBuf.item.GetID() != DataManager.roleEquipment.GetAlternateWeapon().item.GetID())
                        {
                            SelfUpdate();
                            return;
                        }
                        if (ItemBuf.level != DataManager.roleEquipment.GetAlternateWeapon().level)
                        {
                            SelfUpdate();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                break;
            case 2:
                if (Had != DataManager.roleEquipment.HadCuirass())
                {
                    SelfUpdate();
                    return;
                }
                else
                {
                    if (Had)
                    {
                        if (ItemBuf.item.GetID() != DataManager.roleEquipment.GetCuirass().item.GetID())
                        {
                            SelfUpdate();
                            return;
                        }
                        if (ItemBuf.level != DataManager.roleEquipment.GetCuirass().level)
                        {
                            SelfUpdate();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                break;
            case 3:
                if (Had != DataManager.roleEquipment.HadHelm())
                {
                    SelfUpdate();
                    return;
                }
                else
                {
                    if (Had)
                    {
                        if (ItemBuf.item.GetID() != DataManager.roleEquipment.GetHelm().item.GetID())
                        {
                            SelfUpdate();
                            return;
                        }
                        if (ItemBuf.level != DataManager.roleEquipment.GetHelm().level)
                        {
                            SelfUpdate();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                break;
            default:
                if (Had != DataManager.roleEquipment.HadMainWeapon())
                {
                    SelfUpdate();
                    return;
                }
                else
                {
                    if (Had)
                    {
                        if (ItemBuf.item.GetID() != DataManager.roleEquipment.GetMainWeapon().item.GetID())
                        {
                            SelfUpdate();
                            return;
                        }
                        if (ItemBuf.level != DataManager.roleEquipment.GetMainWeapon().level)
                        {
                            SelfUpdate();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                break;
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
        switch (transform.parent.parent.GetSiblingIndex())
        {
            case 0:
                if (DataManager.roleEquipment.HadMainWeapon())
                {
                    Had = true;
                    ItemBuf = DataManager.roleEquipment.GetMainWeapon();
                }
                else
                {
                    Had = false;
                    GetComponent<Canvas>().enabled = false;
                    return;
                }
                break;
            case 1:
                if (DataManager.roleEquipment.HadAlternateWeapon())
                {
                    Had = true;
                    ItemBuf = DataManager.roleEquipment.GetAlternateWeapon();
                }
                else
                {
                    Had = false;
                    GetComponent<Canvas>().enabled = false;
                    return;
                }
                break;
            case 2:
                if (DataManager.roleEquipment.HadCuirass())
                {
                    Had = true;
                    ItemBuf = DataManager.roleEquipment.GetCuirass();
                }
                else
                {
                    Had = false;
                    GetComponent<Canvas>().enabled = false;
                    return;
                }
                break;
            case 3:
                if (DataManager.roleEquipment.HadHelm())
                {
                    Had = true;
                    ItemBuf = DataManager.roleEquipment.GetHelm();
                }
                else
                {
                    Had = false;
                    GetComponent<Canvas>().enabled = false;
                    return;
                }
                break;
            default:
                if (DataManager.roleEquipment.HadMainWeapon())
                {
                    Had = true;
                    ItemBuf = DataManager.roleEquipment.GetMainWeapon();
                }
                else
                {
                    Had = false;
                    GetComponent<Canvas>().enabled = false;
                    return;
                }
                break;
        }
        RareEarthBuf = DataManager.roleEquipment.GetRareEarthCount();
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
}
