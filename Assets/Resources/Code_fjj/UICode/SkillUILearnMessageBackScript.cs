using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUILearnMessageBackScript : MonoBehaviour
{
    public void Click()
    {
        transform.parent.GetComponent<Canvas>().enabled = false;
    }
}
