using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowController : MonoBehaviour
{
    private Attack attack;

    void OnEnable()
    {
        attack = new Attack();
        attack.mTeam = 2;
        Invoke("SaveArrow", 0.7f);       //1秒后回收弓箭
    }
    //超出射程回收弓箭
    void SaveArrow()
    {
        ObjectPool.GetInstant().SaveObj(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * 10 * Time.deltaTime, Space.World);
    }
    //击中目标
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("brave"))
        {
            Life otherLife = other.gameObject.GetComponent<Life>();
            if (otherLife != null)
            {
                attack.attack(otherLife);
            }
            ObjectPool.GetInstant().SaveObj(transform.gameObject);
            if (IsInvoking("SaveArrow"))
            {
                CancelInvoke("SaveArrow");
            }
        }
    }
}
