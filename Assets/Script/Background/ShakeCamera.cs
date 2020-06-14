using UnityEngine;
using System.Collections;
 
public class ShakeCamera : MonoBehaviour
{
    //是否开始抖动
    public bool isShake = false;
    //抖动幅度
    public float shakeLv = 3;
    //设置抖屏的时长
    public float setShakeTime = 0.2f;
    public float shakeFps = 45;
    float fps;
    float shakeTime = 0;
    float frameTime = 0f;
    float shakeDelta = 0.005f;
    //相机
    Camera selfCamers;
    //抖屏幅度
    Rect changeRect;
    void Start()
    {
        selfCamers = GetComponent<Camera>();
        changeRect = new Rect(0, 0, 1, 1);
        shakeTime = setShakeTime;
        fps = shakeFps;
        frameTime = 0.03f;
        shakeDelta = 0.005f;
    }
    void Update()
    {
        if (isShake)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    changeRect.xMin = 0;
                    changeRect.yMin = 0;
                    selfCamers.rect = changeRect;
                    isShake = false;
                    shakeTime = setShakeTime;
                    fps = shakeFps;
                    frameTime = 0.03f;
                    shakeDelta = 0.005f;
                }
                else
                {
                    frameTime += Time.deltaTime;
                    if (frameTime > 1.0 / shakeFps)
                    {
                        frameTime = 0;
                        changeRect.xMin = shakeDelta * (-1 + shakeLv * Random.value);
                        changeRect.yMin = shakeDelta * (-1 + shakeLv * Random.value);
                        selfCamers.rect = changeRect;
                    }
                }
            }
        }
    }
}