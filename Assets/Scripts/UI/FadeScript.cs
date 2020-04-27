using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    float fadeSpeed = 0.01f;
    float R, G, B, A;
    bool isFadeIn;
    bool isFadeOut;

    Image fadeImage;
    [SerializeField] GameObject sceneManager;
    ILoadingScene isLoadingScene;

    void Awake()
    {
        isLoadingScene = sceneManager.GetComponent<ILoadingScene>();
        isFadeIn = true;
        fadeImage = this.gameObject.GetComponent<Image>();
        fadeImage.enabled = true;
        var RGBA = fadeImage.color;
        R = RGBA.r;
        G = RGBA.g;
        B = RGBA.b;
        A = RGBA.a;

    }

    void Update()
    {
        if (isLoadingScene.IsLoadingScene())
        {
            StartFadeOut();
        }
        else if (isFadeIn)
        {
            StartFadeIn();
        }
    }

    void StartFadeIn()
    {
        if (isFadeIn)
        {
            A -= fadeSpeed;
            SetAlpha();
        }

        if (A <= 0.0f)
        {
            isFadeIn = false;
            A = 0.0f;
            //fadeImage.enabled = false;
        }
    }

    void StartFadeOut()
    {
        if (A < 1.0f)
        {
            fadeImage.enabled = true;
            A += fadeSpeed;
            SetAlpha();
        }
    }

    void SetAlpha()
    {
        fadeImage.color = new Color(R, G, B, A);
    }

}