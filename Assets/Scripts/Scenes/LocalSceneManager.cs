using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSceneManager : MonoBehaviour
{
    public SceneData data { get; private set; }

    public void DataGet() { data = MySceneManager.Instance.GetNowStageData(); }

    void Start()
    {
        DataGet();
    }

}