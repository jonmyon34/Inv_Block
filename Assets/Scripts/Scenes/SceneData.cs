public class SceneData
{
    public int sceneState { get; set; }
    public int stageLevel { get; set; }
    public int difficultyLevel { get; set; }
    public float maxTime { get; set; }

    public string sceneName { get; set; }

    public SceneData()
    {
        sceneState = (int)SceneState.Title;
        stageLevel = 0;
        difficultyLevel = 0;
        maxTime = 0.0f;
        sceneName = null;
    }

    public SceneData(int state, int stage, int difficulty, float time, string name)
    {
        sceneState = state;
        stageLevel = stage;
        difficultyLevel = difficulty;
        maxTime = time;
        sceneName = name;
    }
}