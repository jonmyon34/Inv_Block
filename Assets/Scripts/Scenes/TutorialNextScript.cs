using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNextScript : MonoBehaviour
{
    [SerializeField] Text nextScene;
    public bool isDisplayNext = false;

    void Start()
    {
        StartCoroutine("DisplayNextScene");
    }

    void FixedUpdate()
    {
        if (isDisplayNext)
        {
            StartCoroutine("DisplayFlashing");
            isDisplayNext = false;
        }
    }

    IEnumerator DisplayFlashing()
    {
        while (true)
        {
            nextScene.enabled = !nextScene.enabled;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator DisplayNextScene()
    {
        yield return new WaitForSeconds(1.5f);
        nextScene.enabled = true;
        isDisplayNext = true;
    }
}