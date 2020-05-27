using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnightManager : MonoBehaviour
{
    EntityManager generator;
    public float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
            generator = new EntityManager(gameObject.transform.position[0]-10, gameObject.transform.position[0]+ 10, 
                gameObject.transform.position[1],gameObject.transform.position[2], 20, "Knight");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.G))
        {
            generateKnight();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            killAll();
        }
        //move();
         * */
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            generateKnight();
            timer = 3f;
        }
    }
    bool GenerateKnight = false;
    bool KillKnight = false;
    void OnGUI()
    {
        
        GenerateKnight = GUI.Button(new Rect(0, 150, 100, 30), "GenerateKnight");
        KillKnight = GUI.Button(new Rect(0, 180, 100, 30), "KillKnight");
        if (GenerateKnight)
        {
            generateKnight();
        }
        if (KillKnight)
        {
            killAll();
        }
    }

    void killAll()
    {
        for (int i = 0; i < generator.objs.Count; i++)
        {
            GameObject obj = generator.objs[i];
            obj.GetComponent<Knight>().die();
            generator.objs.Remove(obj);

        } 
    }

    void generateKnight()
    {
        GameObject obj = generator.createEntity();
        Knight knight = obj.GetComponent<Knight>();
        knight.init(null, gameObject.transform);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("开始碰撞");
    }

    private float moveSpeed = 3f;
    void move()
    {
        float xm = 0f, ym = 0f, zm = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            zm += moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xm -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zm -= moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xm += moveSpeed * Time.deltaTime;
        }
        gameObject.transform.Translate(new Vector3(xm, ym, zm));
    }
}
