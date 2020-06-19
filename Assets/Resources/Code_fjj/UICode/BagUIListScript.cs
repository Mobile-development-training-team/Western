using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagUIListScript : MonoBehaviour
{
    public GameObject fieldPrefab;
    public static int Buf;

    void Start()
    {
        Buf = DataManager.bag.GetItemBag().Count;
        for (int i = 0; i < Buf; i++)
        {
            GameObject NewOne = Instantiate(fieldPrefab);
            NewOne.transform.SetParent(transform);
            NewOne.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Update()
    {
        if (Buf < DataManager.bag.GetItemBag().Count)
        {
            SelfUpdate();
        }
    }

    private void SelfUpdate()
    {
        for (int i = Buf; i < DataManager.bag.GetItemBag().Count; i++)
        {
            GameObject NewOne = Instantiate(fieldPrefab);
            NewOne.transform.SetParent(transform);
            NewOne.transform.localScale = new Vector3(1, 1, 1);
        }
        Buf = DataManager.bag.GetItemBag().Count;
    }
}
