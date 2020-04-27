using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GameSelectInTitle : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject endButton;
    [SerializeField] GameObject selectSphere;
    [SerializeField] TitleSceneManager titleSceneManager;
    bool isSelectedStart = true;
    bool isSelected = false;

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
    }

    void Start()
    {
        this.UpdateAsObservable()
            .First()
            .Delay(TimeSpan.FromSeconds(1.0f))
            .Subscribe(_ => SelectingStart())
            .AddTo(gameObject);
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !isSelected)
        {
            if (isSelectedStart) { SelectingEnd(); }
            else { SelectingStart(); }
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && !isSelected)
        {
            if (isSelectedStart) { SelectingEnd(); }
            else { SelectingStart(); }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
        {
            SelectNextScene();
        }
    }

    void SelectingStart()
    {
        startButton.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
        endButton.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

        var pos = selectSphere.transform.position;
        pos.y = startButton.transform.position.y;
        selectSphere.transform.position = pos;

        audioSource.PlayOneShot(selectSound);

        isSelectedStart = true;
    }

    void SelectingEnd()
    {
        startButton.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        endButton.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);

        var pos = selectSphere.transform.position;
        pos.y = endButton.transform.position.y;
        selectSphere.transform.position = pos;

        audioSource.PlayOneShot(selectSound);

        isSelectedStart = false;
    }

    void SelectNextScene()
    {
        isSelected = true;
        audioSource.PlayOneShot(decideSound);
        var effect = GameObject.Instantiate(effectPrefab, selectSphere.transform.position, Quaternion.identity);
        selectSphere.SetActive(false);

        if (isSelectedStart) { titleSceneManager.isStageSelect = true; }
        else { titleSceneManager.isEnd = true; }
    }

}