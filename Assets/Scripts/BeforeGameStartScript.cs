using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeforeGameStartScript : MonoBehaviour
{
    [SerializeField] Image readyImage;
    [SerializeField] Image goImage;

    void Start()
    {
        readyImage.enabled = true;
        goImage.enabled = false;
        Time.timeScale = 0.0f;
        StartCoroutine("ReadyGo");
    }

    IEnumerator ReadyGo()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        goImage.enabled = true;
        readyImage.enabled = false;
        yield return new WaitForSecondsRealtime(1.0f);
        goImage.enabled = false;
        Time.timeScale = 1.0f;
    }
}