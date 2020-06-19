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
        GameObject qBuf = transform.Find("Quality").gameObject;
        qBuf.GetComponent<Image>().sprite = GameScript.GetQualitySprite(Buf.item.GetQuality());
        qBuf.transform.Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(Buf.item.GetImagePath());
        if (Buf.item.GetEquipmentOrNot())
        {
            qBuf.transform.Find("Level").GetComponent<Canvas>().enabled = true;
            qBuf.transform.Find("Level").GetComponent<Text>().text = '+' + Buf.level.ToString();
        }
        else
        {
            qBuf.transform.Find("Level").GetComponent<Canvas>().enabled = false;
        }
        qBuf.transform.Find("Text").GetComponent<Text>().text = 'x' + Buf.count.ToString();
    }

    public void Click()
    {
        GameObject mBuf = transform.parent.parent.parent.Find("Message").gameObject;
        mBuf.transform.Find("Make").gameObject.SetActive(false);
        mBuf.transform.Find("Up").gameObject.SetActive(false);
        mBuf.transform.Find("Down").gameObject.SetActive(false);
        mBuf.transform.Find("Resolve").gameObject.SetActive(false);
        mBuf.transform.Find("Intensify").gameObject.SetActive(false);
        if (Buf.item.GetEquipmentOrNot())
        {
            mBuf.transform.Find("Up").gameObject.SetActive(true);
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

            BagUIMessageScript.pastIndex = transform.GetSiblingIndex();

            mBuf.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            mBuf.transform.Find("Make").gameObject.SetActive(true);

            Item iBuf = new Item(DataManager.GameItemIndex, Buf.item.GetID() - 1);
            mBuf.transform.Find("Equipment").Find("Field").Find("Quality").GetComponent<Image>().sprite = GameScript.GetQualitySprite(iBuf.GetQuality());
            mBuf.transform.Find("Equipment").Find("Field").Find("Quality").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(iBuf.GetImagePath());
            mBuf.transform.Find("Equipment").Find("Text").GetComponent<Text>().text = iBuf.GetName() + '\n' + '\n' + iBuf.GetReserveAttribute().ReserveAttributeToString();

            mBuf.transform.Find("Material").Find("List").Find("Debris").gameObject.SetActive(true);

            mBuf.transform.Find("Material").Find("List").Find("Debris").GetComponent<Image>().sprite = GameScript.GetQualitySprite(Buf.item.GetQuality());
            mBuf.transform.Find("Material").Find("List").Find("Debris").Find("Icon").GetComponent<Image>().sprite = GameScript.GetSprite(Buf.item.GetImagePath());
            mBuf.transform.Find("Material").Find("List").Find("Debris").Find("Text").GetComponent<Text>().text = "x15";

            BagUIMessageScript.pastIndex = transform.GetSiblingIndex();

            mBuf.GetComponent<Canvas>().enabled = true;
        }
    }
}
