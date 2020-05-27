using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private GameObject sword;
    private GameObject hand;

    private Vector3[] movePoints;
    private Transform target;
    private float chaseRadius = 10f;
    private float attackRadius = 0.5f;
    private float moveSpeed = 2f;

    private Animator animator;
    private Transform transform;
    private CharacterController controller;

    // Start is called before the first frame update
    public void init(Vector3[] points, Transform target)
    {
        movePoints = points;
        this.target = target;
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        transform = gameObject.transform;
        //controller = gameObject.GetComponent<CharacterController>();
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.name.Equals("GameObject"))
            {
                hand = child.gameObject;
            }
        }
        GameObject swordObj = GameManager.getInstance().loadResource<GameObject>("Sword");
        Debug.Log(swordObj);
        sword = GameObject.Instantiate(swordObj, hand.transform);
        sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath)
        {
            return;
        }
        if (target != null)
        {
            if (isCollisionTarget == true)
            {
                animator.SetTrigger("attack");
            }
            float targetDis = Vector3.Distance(transform.position, target.position);
            if (targetDis <= chaseRadius)
            {
                ToArm();
                if (isCollisionTarget == false)
                {
                    startMove(target.position);
                }
            }
            else
            {
                stopMove();
                unArmed();
            }
        }
        else
        {
            stopMove();
            unArmed();
        }
    }

    private bool isCollisionTarget = false;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            return;
        }
        Debug.Log("开始碰撞");
        stopMove();
        if (collision.gameObject == target.gameObject)
        {
            isCollisionTarget = true;
            attack();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            return;
        }
        if (collision.gameObject == target.gameObject)
        {
            isCollisionTarget = false;
        }
        Debug.Log("离开碰撞");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Plane")
        {
            return;
        }
        Debug.Log("开始触发" + other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Plane")
        {
            return;
        }
        Debug.Log("离开触发");
    }

    void startMove(Vector3 target)
    {
        startWalk();
        Vector3 targetPos = target;
        targetPos[1] = transform.position.y;
        transform.LookAt(targetPos);
        //controller.Move(transform.forward * moveSpeed * Time.deltaTime);
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }

    void stopMove()
    {
        stopWalk();
    }

    private bool isWalking = false;
    private bool isArmed = false;
    private bool isDeath = false;
    void startWalk()
    {
        animator.SetBool("walk", true);
        isWalking = true;
    }

    void stopWalk()
    {
        animator.SetBool("walk", false);
        isWalking = false;
    }

    void ToArm()
    {
        animator.SetBool("arm", true);
        sword.SetActive(true);
        isArmed = true;
    }

    void unArmed()
    {
        animator.SetBool("arm", false);
        sword.SetActive(false);
        isArmed = false;
    }

    void attack()
    {
        animator.SetTrigger("attack");
    }

    public void die()
    {
        if (isDeath == false)
        {
            isDeath = true;
            animator.SetTrigger("death");
            Destroy(this.transform.gameObject,5f);
        }
    }
}

