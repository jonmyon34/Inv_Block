using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    GameObject ballPrefab;
    PlayerHealthManager playerHealthManager;

    public bool isState = false;

    void Awake()
    {
        ballPrefab = (GameObject)Resources.Load("Prefabs/Ball");
        playerHealthManager = this.gameObject.GetComponent<PlayerHealthManager>();
    }

    void Start()
    {
        if (!isState)
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Space) && playerHealthManager.exist)
                .Subscribe(_ => ShootBall())
                .AddTo(gameObject);
        }
        else
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKey(KeyCode.Space) && playerHealthManager.exist)
                .Subscribe(_ => ShootBall())
                .AddTo(gameObject);
        }
    }

    private void ShootBall()
    {
        var pos = this.gameObject.transform.position;
        pos.z += 2.0f;
        Instantiate(ballPrefab, pos, Quaternion.identity);
    }
}