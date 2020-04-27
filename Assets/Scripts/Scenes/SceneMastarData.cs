public class SceneMastarData
{
    public SceneData[] data { get; private set; }

    void DataSet()
    {
        data[0] = new SceneData((int)SceneState.Init,
            (int)StageNumber.Other,
            (int)StageDifficulty.Other,
            (float)StageMaxTime.Other,
            "Init");

        data[1] = new SceneData((int)SceneState.Title,
            (int)StageNumber.Other,
            (int)StageDifficulty.Other,
            (float)StageMaxTime.Other,
            "Title");

        data[2] = new SceneData((int)SceneState.StageSelect,
            (int)StageNumber.Other,
            (int)StageDifficulty.Other,
            (float)StageMaxTime.Other,
            "StageSelect");

        data[3] = new SceneData((int)SceneState.Stage,
            (int)StageNumber.Stage0,
            (int)StageDifficulty.Stage0,
            (float)StageMaxTime.Stage0,
            "Stage000");

        data[4] = new SceneData((int)SceneState.Stage,
            (int)StageNumber.stage1,
            (int)StageDifficulty.Stage1,
            (float)StageMaxTime.Stage1,
            "Stage001");

        data[9] = new SceneData((int)SceneState.Result,
            (int)StageNumber.Other,
            (int)StageDifficulty.Other,
            (float)StageMaxTime.Other,
            "Result");

    }
}

public enum SceneState
{
    Init = 99,
    Title = 0,
    StageSelect,
    Stage,
    Result,
}

public enum StageNumber
{
    Other = 0,
    Stage0,
    stage1,
}

public enum StageDifficulty
{
    Other = 0,
    Stage0,
    Stage1,
}

public enum StageMaxTime
{
    Other = 0,
    Stage0 = 60,
    Stage1 = 60,
    Stage2,
    Stage3,
}