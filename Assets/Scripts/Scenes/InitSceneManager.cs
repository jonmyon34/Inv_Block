using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneManager : MonoBehaviour
{
    SceneData data = new SceneData((int)SceneState.Init,
        (int)StageNumber.Other,
        (int)StageDifficulty.Other,
        (float)StageMaxTime.Other,
        "Init");

    void Start()
    {
        this.UpdateAsObservable()
            .First()
            .Subscribe(_ => Init())
            .AddTo(gameObject);
    }

    void Init()
    {
        //MySceneManager.Instance.LoadNextScene();
        StartCoroutine("LoadTitleScene");
    }

    IEnumerator LoadTitleScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Title");
    }
}