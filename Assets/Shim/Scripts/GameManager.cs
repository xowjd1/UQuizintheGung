using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int stage = 1;
    public int level = 1;

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
