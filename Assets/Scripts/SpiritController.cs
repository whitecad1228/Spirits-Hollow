using System;
using UnityEngine;

public class SpiritController : MonoBehaviour
{
    public float floatSpeed = (float)Math.PI;
    public float floatAmount = 10f;
    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        float x = Mathf.Cos(Time.time * floatSpeed) * floatAmount;
        transform.localPosition = originalPos + new Vector3(x, y, 0f);
    }
}
