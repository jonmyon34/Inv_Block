using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage000SceneManager : MonoBehaviour, ILoadingScene
{
    SceneData data = new SceneData(
        (int)SceneState.Stage,
        (int)StageNumber.Stage0,
        (int)StageDifficulty.Stage0,
        (float)StageMaxTime.Stage0,
        "Stage000");

    public bool isTitle = false;
    //public bool isResult = false;
    public bool isRetry = false;

    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => isTitle)
            .First()
            .Subscribe(_ => StartCoroutine("LoadTitleScene"))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => isRetry)
            .First()
            .Subscribe(_ => StartCoroutine("LoadRetryScene"))
            .AddTo(gameObject);

    }

    IEnumerator LoadTitleScene()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }

    IEnumerator LoadRetryScene()
    {
        //LoadingResultScene();
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Stage000");
    }

    void LoadingResultScene()
    {

    }

    public bool IsLoadingScene()
    {
        if (isTitle || isRetry) { return true; }
        return false;
    }

}