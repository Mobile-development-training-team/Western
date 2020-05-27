using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager
{
    private static EntityManager INSTANCE;

    //private EntityManager() { }

    private Vector3[] vector3s;

    private float x1;
    private float x2;
    private float y;
    private float z;
    private int maxNum;
    private string entityName;
    public EntityManager(float x1, float x2,float y,float z ,int maxNum, string entityName)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.y = y;
        this.z = z;
        this.maxNum = maxNum;
        this.entityName = entityName;
    }

    /*
    public static EntityManager getInstance()
    {
        if(INSTANCE == null)
        {
            INSTANCE = new EntityManager();
        }
        return INSTANCE;
    }
    */
    private GameObject loadObj;
    public List<GameObject> objs = new List<GameObject>();
    public GameObject createEntity()
    {
        if (loadObj == null)
        {
            loadObj = GameManager.getInstance().loadResource<GameObject>(entityName);
        }
        if (objs.Count < maxNum)
        {
            float x = Random.Range(x1, x2);
            Vector3 vector3 = new Vector3(x, y, z);
            GameObject obj = GameObject.Instantiate(loadObj, vector3, new Quaternion(0, 0, 0, 0));
            objs.Add(obj);
            return obj;
        }
        return null;
    }
}
