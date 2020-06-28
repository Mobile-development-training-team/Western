using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIListScript : MonoBehaviour
{
    public GameObject cellPrefab;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject NewOne = Instantiate(cellPrefab);
            NewOne.transform.SetParent(transform);
            NewOne.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
