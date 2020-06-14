using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool _instant;
    //private int AINum = 0;

    public static ObjectPool GetInstant()
    {
        if (_instant == null)
        {
            _instant = new ObjectPool();
        }
        return _instant;
    }

    private Dictionary<string, List<GameObject>> pool;

    private ObjectPool()
    {
        pool = new Dictionary<string, List<GameObject>>();
    }

    public GameObject GetObj(string objName, Vector3 pos, Quaternion qua)
    {
        GameObject currentObj = null;
        //Debug.Log("currentObjName is " + objName);
        if (pool.ContainsKey(objName))
        {
            if(pool[objName].Count>0)
            {
                currentObj=pool[objName][0];
                pool[objName].Remove(currentObj);
            }
        }
        if (currentObj == null)
        {
            currentObj = loadResource<GameObject>(objName);
            //GameObject.Instantiate(currentObj);
            GameObject.Instantiate(currentObj, pos, qua);
            //currentObj = LoadObj(objName); ;
        }
        currentObj.transform.position = pos;
        currentObj.transform.rotation = qua;
        currentObj.SetActive(true);
        //AINum++;//产生AI的数量增加1
        return currentObj;
    }

    public void SaveObj(GameObject obj)
    {
        obj.SetActive(false);
        string objPoolName = obj.name.Replace("(Clone)", "");
        //Debug.Log("saveobj objname "+objPoolName);
        if (pool.ContainsKey(objPoolName))
        {
            pool[objPoolName].Add(obj);
            //Debug.Log("pool size of "+objPoolName+" is "+pool[objPoolName].Count);
        }
        else
        {
            //Debug.Log("does not have objPoolName");
            pool.Add(objPoolName, new List<GameObject>() { obj });
        }
    }
    /*
    public GameObject LoadObj(string objName)
    {
        GameObject currentPrefab = Resources.Load<GameObject>(objName);
        return GameObject.Instantiate(currentPrefab);
    }
    */
    
    public T loadResource<T>(string path) where T : Object
    {
        Object obj = Resources.Load(path);
        if (obj == null)
        {
            return null;
        }
        //Debug.Log(obj.name);
        return (T)obj;
    }
    
    /*
    public void clearObj(string objName)
    {
        AINum--;
    }

    public void clearAll()
    {
        AINum = 0;
    }

    public int GetAINum( )
    {
        return AINum;
    }
    */
}
