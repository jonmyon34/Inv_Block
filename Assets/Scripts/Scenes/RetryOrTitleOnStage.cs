using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryOrTitleOnStage : MonoBehaviour
{
    [SerializeField] Stage000SceneManager sceneManager;

    [SerializeField] Text retryText;
    [SerializeField] Text titleText;
    bool isRetry = false;
    bool isTitle = false;

    public bool isStartResult = false;

    bool isSelected = false;

    Vector3 defaultScale;

    AudioSource audioSource;
    AudioClip selectSound;
    AudioClip decideSound;
    GameObject effectPrefab;

    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        selectSound = (AudioClip)Resources.Load("Sounds/Select");
        decideSound = (AudioClip)Resources.Load("Sounds/Explosion");
        effectPrefab = (GameObject)Resources.Load("Prefabs/BigExplosionCustomed");

        defaultScale = retryText.transform.localScale;

        retryText.enabled = false;
        titleText.enabled = false;
    }

    void Update()
    {
        if (isStartResult && !isSelected)
        {
            retryText.enabled = true;
            titleText.enabled = true;
            SelectNextMode();
        }
    }

    void SelectNextMode()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !isSelected)
        {
            SelectingRetry();
        }

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !isSelected)
        {
            SelectingTitle();
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E)) && (isTitle || isRetry))
        {
            SelectNextScene();
        }

    }

    void SelectingRetry()
    {
        retryText.transform.localScale = defaultScale * (3.0f / 2.0f);
        titleText.transform.localScale = defaultScale;

        audioSource.PlayOneShot(selectSound);

        isRetry = true;
        isTitle = false;
    }

    void SelectingTitle()
    {
        titleText.transform.localScale = defaultScale * (3.0f / 2.0f);
        retryText.transform.localScale = defaultScale;

        audioSource.PlayOneShot(selectSound);

        isTitle = true;
        isRetry = false;
    }

    void SelectNextScene()
    {
        isSelected = true;
        audioSource.PlayOneShot(decideSound);

        if (isRetry) { sceneManager.isRetry = true; }
        else if (isTitle) { sceneManager.isTitle = true; }

    }
}