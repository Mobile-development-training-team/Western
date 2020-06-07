using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform character;
    Vector3 distance;
    public float h_speed = 1f;
    public float x_speed = 2f;
    Vector3 ve;

    private void Start()
    {
        distance = transform.position - character.position;
    }

    public void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, character.position + distance,ref ve,0);
    }
}
