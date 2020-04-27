using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTimeCounter : MonoBehaviour
{
    public float time { get; private set; }

    void Start()
    {
        time = 0.0f;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
    }
}