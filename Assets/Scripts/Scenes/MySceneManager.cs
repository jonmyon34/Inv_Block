using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonMonoBehaviour<MySceneManager>
{
    private SceneData[] sceneData;
    private int nowStageNum;
    private int stageMaxNum;
    private int sceneMaxNum;

    public SceneData GetNowStageData() { return sceneData[nowStageNum]; }

    override protected void Awake()
    {
        base.Awake();

        if (this != null)
        {
            DataSet();
            SetNowStageNum();
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }

    void SetNowStageNum()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != null) { Debug.Log(sceneName); }

        for (int i = 0; i < 10; i++)
        {
            if (sceneData[i] == null) { Debug.Log("Null"); continue; }
            else if (sceneData[i].sceneName == sceneName) { nowStageNum = i; Debug.Log(i); break; }
        }
    }

    public void LoadNextScene()
    {
        nowStageNum++;
        SceneManager.LoadScene(sceneData[nowStageNum].sceneName);
    }

    public void LoadSpecifiedScene(int sceneNumber)
    {
        nowStageNum = sceneNumber;
        SceneManager.LoadScene(sceneData[nowStageNum].sceneName);
    }

    void DataSet()
    {

        sceneData[0] = new SceneData((int)SceneState.Init,
            (int)StageNumber.Other,
            (int)StageDifficulty.Other,
            (float)StageMaxTime.Other,
            "Init");

        sceneData[1] = new SceneData((int)SceneState.Title,
            (int)StageNumber.Other,
            (int)StageDifficulty.Other,
            (float)StageMaxTime.Other,
            "Title");

        sceneData[2] = new SceneData((int)SceneState.StageSelect,
            (int)StageNumber.Other,
            (int)StageDifficulty.Other,
            (float)StageMaxTime.Other,
            "StageSelect");

        sceneData[3] = new SceneData((int)SceneState.Stage,
            (int)StageNumber.Stage0,
            (int)StageDifficulty.Stage0,
            (float)StageMaxTime.Stage0,
            "Stage000");

        sceneData[4] = new SceneData((int)SceneState.Stage,
            (int)StageNumber.stage1,
            (int)StageDifficulty.Stage1,
            (float)StageMaxTime.Stage1,
            "Stage001");

        sceneData[9] = new SceneData((int)SceneState.Result,
            (int)StageNumber.Other,
            (int)StageDifficulty.Other,
            (float)StageMaxTime.Other,
            "Result");

    }

}