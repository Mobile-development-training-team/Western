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

        GameObject Buf = transform.Find("Quality").gameObject;
        switch (transform.GetSiblingIndex())
        {
            case 0:
                if (bBuf[0])
                {
                    Buf.GetComponent<Image>().sprite = GameScript.GetQualitySprite(biBuf[0].item.GetQuality());
                    Buf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(biBuf[0].item.GetImagePath());
                    Buf.transform.Find("Level").GetComponent<Text>().text = '+' + biBuf[0].level.ToString();
                    Buf.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    Buf.GetComponent<Canvas>().enabled = false;
                }
                break;
            case 1:
                if (bBuf[1])
                {
                    Buf.GetComponent<Image>().sprite = GameScript.GetQualitySprite(biBuf[1].item.GetQuality());
                    Buf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(biBuf[1].item.GetImagePath());
                    Buf.transform.Find("Level").GetComponent<Text>().text = '+' + biBuf[1].level.ToString();
                    Buf.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    Buf.GetComponent<Canvas>().enabled = false;
                }
                break;
            case 2:
                if (bBuf[2])
                {
                    Buf.GetComponent<Image>().sprite = GameScript.GetQualitySprite(biBuf[2].item.GetQuality());
                    Buf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(biBuf[2].item.GetImagePath());
                    Buf.transform.Find("Level").GetComponent<Text>().text = '+' + biBuf[2].level.ToString();
                    Buf.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    Buf.GetComponent<Canvas>().enabled = false;
                }
                break;
            case 3:
                if (bBuf[3])
                {
                    Buf.GetComponent<Image>().sprite = GameScript.GetQualitySprite(biBuf[3].item.GetQuality());
                    Buf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(biBuf[3].item.GetImagePath());
                    Buf.transform.Find("Level").GetComponent<Text>().text = '+' + biBuf[3].level.ToString();
                    Buf.GetComponent<Canvas>().enabled = true;
                }
                else
                {
                    Buf.GetComponent<Canvas>().enabled = false;
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

        GameObject mBuf = transform.parent.parent.Find("Message").gameObject;

        mBuf.transform.Find("Make").gameObject.SetActive(false);
        mBuf.transform.Find("Up").gameObject.SetActive(false);
        mBuf.transform.Find("Down").gameObject.SetActive(false);
        mBuf.transform.Find("Resolve").gameObject.SetActive(false);
        mBuf.transform.Find("Intensify").gameObject.SetActive(false);

        BagUIMessageScript.pastIndex = transform.GetSiblingIndex() + DataManager.bag.GetItemBag().Count;

        mBuf.transform.Find("Down").gameObject.SetActive(true);
        mBuf.transform.Find("Resolve").gameObject.SetActive(true);
        mBuf.transform.Find("Intensify").gameObject.SetActive(true);

        mBuf.transform.Find("Equipment").Find("Field").Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(Buf.item.GetQuality());
        mBuf.transform.Find("Equipment").Find("Field").Find("Quality").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(Buf.item.GetImagePath());
        mBuf.transform.Find("Equipment").Find("Text").GetComponent<Text>().text = Buf.item.GetName() + '\n' + '\n' + Buf.GetEquipmentReserveAttribute().ReserveAttributeToString();

        mBuf.transform.Find("Material").Find("List").Find("Debris").gameObject.SetActive(true);
        mBuf.transform.Find("Material").Find("List").Find("RareEarth").gameObject.SetActive(true);
        Item iBuf = new Item(DataManager.GameItemIndex, Buf.item.GetID() + 1);
        mBuf.transform.Find("Material").Find("List").Find("Debris").GetComponent<Image>().sprite = GameScript.GetQualitySprite(iBuf.GetQuality());
        mBuf.transform.Find("Material").Find("List").Find("Debris").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(iBuf.GetImagePath());
        mBuf.transform.Find("Material").Find("List").Find("Debris").Find("Text").GetComponent<Text>().text = 'x' + Buf.GetIntensifyDebris().ToString();
        mBuf.transform.Find("Material").Find("List").Find("RareEarth").Find("Text").GetComponent<Text>().text = 'x' + Buf.GetIntensifyRareEarth().ToString();

        mBuf.GetComponent<Canvas>().enabled = true;


    }
}

