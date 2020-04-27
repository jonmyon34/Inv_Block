using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpeed : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-1.3f, 1.3f), 0.0f, Random.Range(0.8f, 1.3f)) * 10.0f;
    }

}