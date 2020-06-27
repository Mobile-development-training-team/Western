using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUIListFieldScript : MonoBehaviour
{
    private BagItem Buf;

    private void Start()
    {
        Buf = new BagItem();
        Buf.item = new Item();
        SelfUpdate();
    }

    private void Update()
    {
        if (transform.GetSiblingIndex() >= DataManager.bag.GetItemBag().Count)
        {
            BagUIListScript.Buf--;
            Destroy(gameObject);
        }
        if (Buf.item.GetID() != DataManager.bag.GetItemBag()[transform.GetSiblingIndex()].item.GetID())
        {
            SelfUpdate();
            return;
        }
        if (Buf.level != DataManager.bag.GetItemBag()[transform.GetSiblingIndex()].level)
        {
            SelfUpdate();
            return;
        }
        if (Buf.count != DataManager.bag.GetItemBag()[transform.GetSiblingIndex()].count)
        {
            SelfUpdate();
            return;
        }
    }

    private void SelfUpdate()
    {
        Buf.item = DataManager.bag.GetItemBag()[transform.GetSiblingIndex()].item;
        Buf.level = DataManager.bag.GetItemBag()[transform.GetSiblingIndex()].level;
        Buf.count = DataManager.bag.GetItemBag()[transform.GetSiblingIndex()].count;
        transform.Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(Buf.item.GetQuality());
        transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(Buf.item.GetImagePath());
        if (Buf.item.GetEquipmentOrNot())
        {
            transform.Find("Lv").GetComponent<Canvas>().enabled = true;
            transform.Find("Lv").GetComponent<Text>().text = '+' + Buf.level.ToString();
        }
        else
        {
            transform.Find("Lv").GetComponent<Canvas>().enabled = false;
        }
        transform.Find("Count").GetComponent<Text>().text = 'x' + Buf.count.ToString();
    }

    public void Click()
    {
        GameObject ObjBuf = transform.parent.parent.parent.parent.parent.parent.Find("ItemMessage").gameObject;
        GameObject aBuf = ObjBuf.transform.Find("Attribute").gameObject;
        GameObject cBuf = ObjBuf.transform.Find("Cost").gameObject;
        cBuf.transform.Find("RareEarth").gameObject.SetActive(false);
        GameObject ButtonBuf = ObjBuf.transform.Find("Button").gameObject;
        ButtonBuf.transform.Find("Make").gameObject.SetActive(false);
        ButtonBuf.transform.Find("Up").gameObject.SetActive(false);
        ButtonBuf.transform.Find("Down").gameObject.SetActive(false);
        ButtonBuf.transform.Find("Resolve").gameObject.SetActive(false);
        ButtonBuf.transform.Find("Intensify").gameObject.SetActive(false);
        if (Buf.item.GetEquipmentOrNot())
        {
            ButtonBuf.transform.Find("Up").gameObject.SetActive(true);
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

            BagUIMessageScript.pastIndex = transform.GetSiblingIndex();

        }
        else
        {
            ButtonBuf.transform.Find("Make").gameObject.SetActive(true);

            Item iBuf = new Item(DataManager.GameItemIndex, Buf.item.GetID() - 1);
            ObjBuf.transform.Find("Field").Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(iBuf.GetQuality());
            ObjBuf.transform.Find("Field").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(iBuf.GetImagePath());
            ObjBuf.transform.Find("Field").Find("Lv").GetComponent<Text>().text = "+0";
            ObjBuf.transform.Find("Field").Find("Count").GetComponent<Text>().text = "x1";

            ObjBuf.transform.Find("Name").GetComponent<Text>().text = iBuf.GetName();

            if (iBuf.GetReserveAttribute().AddATK > 0 || iBuf.GetReserveAttribute().PlusATK > 0.000001)
            {
                aBuf.transform.Find("ATK").gameObject.SetActive(true);
                aBuf.transform.Find("ATK").Find("Text").GetComponent<Text>().text = iBuf.GetReserveAttribute().AtkToString();
                aBuf.transform.Find("ATK").Find("Intensify").GetComponent<Canvas>().enabled = false;
            }
            else
            {
                aBuf.transform.Find("ATK").Find("Intensify").GetComponent<Canvas>().enabled = false;
                aBuf.transform.Find("ATK").gameObject.SetActive(false);
            }

            if (iBuf.GetReserveAttribute().AddHP > 0 || iBuf.GetReserveAttribute().PlusHP > 0.000001)
            {
                aBuf.transform.Find("HP").gameObject.SetActive(true);
                aBuf.transform.Find("HP").Find("Text").GetComponent<Text>().text = iBuf.GetReserveAttribute().HpToString();
                aBuf.transform.Find("HP").Find("Intensify").GetComponent<Canvas>().enabled = false;
            }
            else
            {
                aBuf.transform.Find("HP").Find("Intensify").GetComponent<Canvas>().enabled = false;
                aBuf.transform.Find("HP").gameObject.SetActive(false);
            }

            if (iBuf.GetReserveAttribute().AddDEF > 0 || iBuf.GetReserveAttribute().PlusDEF > 0.000001)
            {
                aBuf.transform.Find("DEF").gameObject.SetActive(true);
                aBuf.transform.Find("DEF").Find("Text").GetComponent<Text>().text = iBuf.GetReserveAttribute().DefToString();
                aBuf.transform.Find("DEF").Find("Intensify").GetComponent<Canvas>().enabled = false;
            }
            else
            {
                aBuf.transform.Find("DEF").Find("Intensify").GetComponent<Canvas>().enabled = false;
                aBuf.transform.Find("DEF").gameObject.SetActive(false);
            }

            cBuf.transform.Find("Debris").Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(Buf.item.GetQuality());
            cBuf.transform.Find("Debris").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(Buf.item.GetImagePath());
            cBuf.transform.Find("Debris").Find("Count").GetComponent<Text>().text = "x15";

            BagUIMessageScript.pastIndex = transform.GetSiblingIndex();
        }
        ObjBuf.GetComponent<Canvas>().enabled = true;
    }
}
