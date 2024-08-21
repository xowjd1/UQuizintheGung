using System;
using System.Collections.Generic;
using UnityEngine;

public class QuizPositionController : MonoBehaviour
{
    public static QuizPositionController Instance;
    public List<Transform> positionList = new List<Transform>();
    
    private int _nextIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        foreach (Transform transform in positionList)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void ActiveNextPosition()
    {
        positionList[_nextIndex].gameObject.SetActive(true);
    }
    
    public void DeActivePosition()
    {
        positionList[_nextIndex].gameObject.SetActive(false);
    }

    public Vector3 GetNextPosition()
    {
        return positionList[_nextIndex].position;
    }
    
    public void SetNextPosition()
    {
        if (_nextIndex < positionList.Count - 1) _nextIndex++;
    }
}
