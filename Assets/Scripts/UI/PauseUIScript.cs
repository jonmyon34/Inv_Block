using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIScript : MonoBehaviour
{
    GameObject pauseUIPrefab;
    GameObject pauseUIInstance;
    bool isCanPause;

    void Start()
    {
        var canPose = this.gameObject.GetComponent<IToTitleSceneInPause>();
        if (canPose != null)
        {
            isCanPause = true;
            pauseUIPrefab = (GameObject)Resources.Load("Prefabs/PauseUI");
        }
        else { isCanPause = false; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCanPause)
        {
            Pause();
        }
    }

    void Pause()
    {
        if (pauseUIInstance == null)
        {
            pauseUIInstance = GameObject.Instantiate(pauseUIPrefab)as GameObject;
            Time.timeScale = 0.0f;
        }
        else
        {
            Destroy(pauseUIInstance);
            Time.timeScale = 1.0f;
        }
    }

}