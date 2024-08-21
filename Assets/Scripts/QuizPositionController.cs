using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizPositionController : MonoBehaviour
{
    public List<Transform> positionList = new List<Transform>();
    private int _nextIndex = 1;

    public int NextIndex
    {
        get => _nextIndex;
    }

    public void IncreaseNextIndex()
    {
        if (_nextIndex < positionList.Count - 1) _nextIndex++;
    }

    public Vector3 GetPosition(int idx)
    {
        return positionList[idx].position;
    }
}
