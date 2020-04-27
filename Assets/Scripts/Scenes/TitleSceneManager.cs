using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour, ILoadingScene
{
    SceneData data = new SceneData(
        (int)SceneState.Title,
        (int)StageNumber.Other,
        (int)StageDifficulty.Other,
        (float)StageMaxTime.Other,
        "Title");

    public bool isEnd = false;
    public bool isStageSelect = false;

    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.Escape))
            .Subscribe(_ => isEnd = true)
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => isEnd)
            .First()
            .Subscribe(_ => StartCoroutine("Quit"))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => isStageSelect)
            .First()
            .Subscribe(_ => StartCoroutine("LoadStageSelect"))
            .AddTo(gameObject);
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(2.5f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif 
    }

    IEnumerator LoadStageSelect()
    {
        Debug.Log("Next is StageSelect");
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("StageSelect");
    }

    public bool IsLoadingScene()
    {
        if (isEnd || isStageSelect) { return true; }
        return false;
    }
}