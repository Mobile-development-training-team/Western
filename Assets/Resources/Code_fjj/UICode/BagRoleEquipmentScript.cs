using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagRoleEquipmentScript : MonoBehaviour
{
    private BagItem[] biBuf = new BagItem[4];
    private bool[] bBuf = new bool[4];

    private void Start()
    {
        SelfUpdate();
    }

    private void Update()
    {
        if (bBuf[0] != DataManager.roleEquipment.HadMainWeapon())
        {
            SelfUpdate();
            return;
        }
        if (bBuf[1] != DataManager.roleEquipment.HadAlternateWeapon())
        {
            SelfUpdate();
            return;
        }
        if (bBuf[2] != DataManager.roleEquipment.HadCuirass())
        {
            SelfUpdate();
            return;
        }
        if (bBuf[3] != DataManager.roleEquipment.HadHelm())
        {
            SelfUpdate();
            return;
        }
        if (bBuf[0] && (biBuf[0].item.GetID() != DataManager.roleEquipment.GetMainWeapon().item.GetID() || biBuf[0].level != DataManager.roleEquipment.GetMainWeapon().level))
        {
            SelfUpdate();
            return;
        }
        if (bBuf[1] && (biBuf[1].item.GetID() != DataManager.roleEquipment.GetAlternateWeapon().item.GetID() || biBuf[1].level != DataManager.roleEquipment.GetAlternateWeapon().level))
        {
            SelfUpdate();
            return;
        }
        if (bBuf[2] && (biBuf[2].item.GetID() != DataManager.roleEquipment.GetCuirass().item.GetID() || biBuf[2].level != DataManager.roleEquipment.GetCuirass().level))
        {
            SelfUpdate();
            return;
        }
        if (bBuf[3] && (biBuf[3].item.GetID() != DataManager.roleEquipment.GetHelm().item.GetID() || biBuf[3].level != DataManager.roleEquipment.GetHelm().level))
        {
            SelfUpdate();
            return;
        }
    }

    private void SelfUpdate()
    {
        bBuf[0] = DataManager.roleEquipment.HadMainWeapon();
        bBuf[1] = DataManager.roleEquipment.HadAlternateWeapon();
        bBuf[2] = DataManager.roleEquipment.HadCuirass();
        bBuf[3] = DataManager.roleEquipment.HadHelm();

        if (bBuf[0])
        {
            biBuf[0].item = new Item();
            biBuf[0].item = DataManager.roleEquipment.GetMainWeapon().item;
            biBuf[0].level = DataManager.roleEquipment.GetMainWeapon().level;
            biBuf[0].count = DataManager.roleEquipment.GetMainWeapon().count;
        }
        if (bBuf[1])
        {
            biBuf[1].item = new Item();
            biBuf[1].item = DataManager.roleEquipment.GetAlternateWeapon().item;
            biBuf[1].level = DataManager.roleEquipment.GetAlternateWeapon().level;
            biBuf[1].count = DataManager.roleEquipment.GetAlternateWeapon().count;
        }
        if (bBuf[2])
        {
            biBuf[2].item = new Item();
            biBuf[2].item = DataManager.roleEquipment.GetCuirass().item;
            biBuf[2].level = DataManager.roleEquipment.GetCuirass().level;
            biBuf[2].count = DataManager.roleEquipment.GetCuirass().count;
        }
        if (bBuf[3])
        {
            biBuf[3].item = new Item();
            biBuf[3].item = DataManager.roleEquipment.GetHelm().item;
            biBuf[3].level = DataManager.roleEquipment.GetHelm().level;
            biBuf[3].count = DataManager.roleEquipment.GetHelm().count;
        }

        GameObject hBuf = transform.Find("Had").gameObject;
        switch (transform.GetSiblingIndex())
        {
            case 0:
                if (bBuf[0])
                {
                    hBuf.transform.Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(biBuf[0].item.GetQuality());
                    hBuf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(biBuf[0].item.GetImagePath());
                    hBuf.transform.Find("Lv").GetComponent<Text>().text = '+' + biBuf[0].level.ToString();
                    hBuf.transform.Find("Count").GetComponent<Text>().text = 'x' + biBuf[0].count.ToString();
                    hBuf.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    hBuf.GetComponent<Canvas>().enabled = false;
                }
                break;
            case 1:
                if (bBuf[1])
                {
                    hBuf.transform.Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(biBuf[1].item.GetQuality());
                    hBuf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(biBuf[1].item.GetImagePath());
                    hBuf.transform.Find("Lv").GetComponent<Text>().text = '+' + biBuf[1].level.ToString();
                    hBuf.transform.Find("Count").GetComponent<Text>().text = 'x' + biBuf[1].count.ToString();
                    hBuf.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    hBuf.GetComponent<Canvas>().enabled = false;
                }
                break;
            case 2:
                if (bBuf[2])
                {
                    hBuf.transform.Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(biBuf[2].item.GetQuality());
                    hBuf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(biBuf[2].item.GetImagePath());
                    hBuf.transform.Find("Lv").GetComponent<Text>().text = '+' + biBuf[2].level.ToString();
                    hBuf.transform.Find("Count").GetComponent<Text>().text = 'x' + biBuf[2].count.ToString();
                    hBuf.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    hBuf.GetComponent<Canvas>().enabled = false;
                }
                break;
            case 3:
                if (bBuf[3])
                {
                    hBuf.transform.Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(biBuf[3].item.GetQuality());
                    hBuf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(biBuf[3].item.GetImagePath());
                    hBuf.transform.Find("Lv").GetComponent<Text>().text = '+' + biBuf[3].level.ToString();
                    hBuf.transform.Find("Count").GetComponent<Text>().text = 'x' + biBuf[3].count.ToString();
                    hBuf.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    hBuf.GetComponent<Canvas>().enabled = false;
                }
                break;
        }
    }

    public void Click()
    {
        BagItem Buf;
        switch (transform.GetSiblingIndex())
        {
            case 0:
                if (DataManager.roleEquipment.HadMainWeapon())
                {
                    Buf = DataManager.roleEquipment.GetMainWeapon();
                    if (GameScript.EquipmentModel[1] != null)
                    {
                        GameScript.EquipmentModel[1].SetActive(false);
                    }
                    GameScript.EquipmentModel[0].SetActive(true);
                }
                else
                {
                    return;
                }
                break;
            case 1:
                if (DataManager.roleEquipment.HadAlternateWeapon())
                {
                    Buf = DataManager.roleEquipment.GetAlternateWeapon();
                    if (GameScript.EquipmentModel[0] != null)
                    {
                        GameScript.EquipmentModel[0].SetActive(false);
                    }
                    GameScript.EquipmentModel[1].SetActive(true);
                }
                else
                {
                    return;
                }
                break;
            case 2:
                if (DataManager.roleEquipment.HadCuirass())
                {
                    Buf = DataManager.roleEquipment.GetCuirass();
                }
                else
                {
                    return;
                }
                break;
            case 3:
                if (DataManager.roleEquipment.HadHelm())
                {
                    Buf = DataManager.roleEquipment.GetHelm();
                }
                else
                {
                    return;
                }
                break;
            default:
                if (DataManager.roleEquipment.HadMainWeapon())
                {
                    Buf = DataManager.roleEquipment.GetMainWeapon();
                }
                else
                {
                    return;
                }
                break;
        }

        GameObject ObjBuf = transform.parent.parent.parent.parent.Find("ItemMessage").gameObject;
        GameObject aBuf = ObjBuf.transform.Find("Attribute").gameObject;
        GameObject cBuf = ObjBuf.transform.Find("Cost").gameObject;
        cBuf.transform.Find("RareEarth").gameObject.SetActive(false);
        GameObject ButtonBuf = ObjBuf.transform.Find("Button").gameObject;
        ButtonBuf.transform.Find("Make").gameObject.SetActive(false);
        ButtonBuf.transform.Find("Up").gameObject.SetActive(false);
        ButtonBuf.transform.Find("Down").gameObject.SetActive(false);
        ButtonBuf.transform.Find("Resolve").gameObject.SetActive(false);
        ButtonBuf.transform.Find("Intensify").gameObject.SetActive(false);

        ButtonBuf.transform.Find("Down").gameObject.SetActive(true);
        ButtonBuf.transform.Find("Resolve").gameObject.SetActive(true);
        ButtonBuf.transform.Find("Intensify").gameObject.SetActive(true);

        ObjBuf.transform.Find("Field").Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(Buf.item.GetQuality());
        ObjBuf.transform.Find("Field").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(Buf.item.GetImagePath());
        ObjBuf.transform.Find("Field").Find("Lv").GetComponent<Text>().text = '+' + Buf.level.ToString();
        ObjBuf.transform.Find("Field").Find("Count").GetComponent<Text>().text = 'x' + Buf.count.ToString();

        ObjBuf.transform.Find("Name").GetComponent<Text>().text = Buf.item.GetName();

        if (Buf.GetEquipmentReserveAttribute().AddATK > 0 || Buf.GetEquipmentReserveAttribute().PlusATK > 0.000001)
        {
            aBuf.transform.Find("ATK").gameObject.SetActive(true);
            aBuf.transform.Find("ATK").Find("Text").GetComponent<Text>().text = Buf.GetEquipmentReserveAttribute().AtkToString();
            if (Buf.level < 20)
            {
                aBuf.transform.Find("ATK").Find("Intensify").GetComponent<Canvas>().enabled = true;
                aBuf.transform.Find("ATK").Find("Intensify").Find("Next").GetComponent<Text>().text = (Buf.GetEquipmentReserveAttribute() * 1.1).AtkToString();
            }
        }
        else
        {
            aBuf.transform.Find("ATK").Find("Intensify").GetComponent<Canvas>().enabled = false;
            aBuf.transform.Find("ATK").gameObject.SetActive(false);
        }

        if (Buf.GetEquipmentReserveAttribute().AddHP > 0 || Buf.GetEquipmentReserveAttribute().PlusHP > 0.000001)
        {
            aBuf.transform.Find("HP").gameObject.SetActive(true);
            aBuf.transform.Find("HP").Find("Text").GetComponent<Text>().text = Buf.GetEquipmentReserveAttribute().HpToString();
            if (Buf.level < 20)
            {
                aBuf.transform.Find("HP").Find("Intensify").GetComponent<Canvas>().enabled = true;
                aBuf.transform.Find("HP").Find("Intensify").Find("Next").GetComponent<Text>().text = (Buf.GetEquipmentReserveAttribute() * 1.1).HpToString();
            }
        }
        else
        {
            aBuf.transform.Find("HP").Find("Intensify").GetComponent<Canvas>().enabled = false;
            aBuf.transform.Find("HP").gameObject.SetActive(false);
        }

        if (Buf.GetEquipmentReserveAttribute().AddDEF > 0 || Buf.GetEquipmentReserveAttribute().PlusDEF > 0.000001)
        {
            aBuf.transform.Find("DEF").gameObject.SetActive(true);
            aBuf.transform.Find("DEF").Find("Text").GetComponent<Text>().text = Buf.GetEquipmentReserveAttribute().DefToString();
            if (Buf.level < 20)
            {
                aBuf.transform.Find("DEF").Find("Intensify").GetComponent<Canvas>().enabled = true;
                aBuf.transform.Find("DEF").Find("Intensify").Find("Next").GetComponent<Text>().text = (Buf.GetEquipmentReserveAttribute() * 1.1).DefToString();
            }
        }
        else
        {
            aBuf.transform.Find("DEF").Find("Intensify").GetComponent<Canvas>().enabled = false;
            aBuf.transform.Find("DEF").gameObject.SetActive(false);
        }

        cBuf.transform.Find("RareEarth").gameObject.SetActive(true);
        Item iBuf = new Item(DataManager.GameItemIndex, Buf.item.GetID() + 1);
        cBuf.transform.Find("Debris").Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(iBuf.GetQuality());
        cBuf.transform.Find("Debris").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(iBuf.GetImagePath());
        cBuf.transform.Find("Debris").Find("Count").GetComponent<Text>().text = 'x' + Buf.GetIntensifyDebris().ToString();
        cBuf.transform.Find("RareEarth").gameObject.SetActive(true);
        cBuf.transform.Find("RareEarth").Find("Count").GetComponent<Text>().text = 'x' + Buf.GetIntensifyRareEarth().ToString();


        BagUIMessageScript.pastIndex = transform.GetSiblingIndex() + DataManager.bag.GetItemBag().Count;

        ObjBuf.GetComponent<Canvas>().enabled = true;
    }
}

