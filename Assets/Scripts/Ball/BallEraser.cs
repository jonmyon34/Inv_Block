using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BallEraser : MonoBehaviour
{
    void Start()
    {
        this.UpdateAsObservable()
            .First(_ => Vector3.Distance(Vector3.zero, this.gameObject.transform.position) > 50.0f)
            .Subscribe(_ => Destroy(this.gameObject))
            .AddTo(gameObject);
    }

}