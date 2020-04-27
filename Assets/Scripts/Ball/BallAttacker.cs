using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttacker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var objAddDamage = collision.gameObject.GetComponent<IDamagable>();
        if(objAddDamage != null)
        {
            collision.gameObject.GetComponent<IDamagable>().AddDamage();
        }
    }
}
