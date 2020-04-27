using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectSceneManager : MonoBehaviour, ILoadingScene
{
    SceneData data = new SceneData(
        (int)SceneState.StageSelect,
        (int)StageNumber.Other,
        (int)StageDifficulty.Other,
        (float)StageMaxTime.Other,
        "StageSelect");

    public bool isTitle = false;
    public bool isStage000 = false;

    public void IsTitleTrue() { isTitle = true; }
    public void IsStage000True() { isStage000 = true; }

    AudioSource audioSource;
    AudioClip decideSound;

    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        decideSound = (AudioClip)Resources.Load("Sounds/Explosion");
    }

    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.Escape))
            .Subscribe(_ => isTitle = true)
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
            .Subscribe(_ => isStage000 = true)
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => isTitle)
            .First()
            .Subscribe(_ => StartCoroutine("LoadTitleScene"))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => isStage000)
            .First()
            .Subscribe(_ => StartCoroutine("LoadStage000"))
            .AddTo(gameObject);
    }

    IEnumerator LoadTitleScene()
    {
        audioSource.PlayOneShot(decideSound);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Title");
    }

    IEnumerator LoadStage000()
    {
        audioSource.PlayOneShot(decideSound);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Stage000");
    }

    public bool IsLoadingScene()
    {
        if (isTitle || isStage000) { return true; }
        return false;
    }
}