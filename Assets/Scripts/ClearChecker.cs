using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class ClearChecker : MonoBehaviour
{
    [SerializeField] GameObject blockParent;
    [SerializeField] PlayerHealthManager player;
    public bool isClear = false;
    public bool isFailed = false;
    int plHp;

    AudioSource audioSource;
    AudioClip gameOverSound;

    [SerializeField] Image clearImage;
    [SerializeField] Image failedImage;

    RetryOrTitleOnStage retryOrTitle;

    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        gameOverSound = (AudioClip)Resources.Load("Sounds/GameOver");

        clearImage.enabled = false;
        failedImage.enabled = false;

        retryOrTitle = this.gameObject.GetComponent<RetryOrTitleOnStage>();
    }

    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => isClear)
            .First()
            .Subscribe(_ => StartCoroutine("StartClearMode"))
            .AddTo(gameObject);

        this.UpdateAsObservable()
            .Where(_ => isFailed)
            .First()
            .Subscribe(_ => StartCoroutine("StartFailedMode"))
            .AddTo(gameObject);
    }

    void FixedUpdate()
    {
        GameStateCheck();
    }

    void GameStateCheck()
    {
        if (blockParent.transform.childCount == 0 && player.GetHp() > 0)
        {
            isClear = true;
        }

        if (player.GetHp() <= 0 && blockParent.transform.childCount > 0)
        {
            isFailed = true;
        }
    }

    IEnumerator StartClearMode()
    {
        Time.timeScale = 0.0f;
        audioSource.PlayOneShot(gameOverSound);
        yield return new WaitForSecondsRealtime(2.0f);

        DisplayClear();

        yield return new WaitForSecondsRealtime(1.0f);
        retryOrTitle.isStartResult = true;
    }

    IEnumerator StartFailedMode()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        audioSource.PlayOneShot(gameOverSound);
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(2.0f);

        DisplayFailed();

        yield return new WaitForSecondsRealtime(1.0f);
        retryOrTitle.isStartResult = true;
    }

    void DisplayClear()
    {
        clearImage.enabled = true;
    }

    void DisplayFailed()
    {
        failedImage.enabled = true;
    }
}