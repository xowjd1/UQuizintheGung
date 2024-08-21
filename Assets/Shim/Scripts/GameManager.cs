public class GameManager : Singleton<GameManager>
{
    public int stage = 1;
    public int level = 1;
    public bool activeRayCast = true;
    public bool open = false;

    public bool levelCounter = false;
    public bool stageCounter = false;
    
    public int answerCounter = 0;

    public int maxCounter = 8; 

    public void AddLevel(int level)
    { 
        this.level = level;
    }

    public void AddStage(int stage)
    { 
        this.stage = stage;
    }

    public int GetLevel()
    { 
        return this.level;
    }

    public int GetStage()
    {
        return this.stage;
    }
}
