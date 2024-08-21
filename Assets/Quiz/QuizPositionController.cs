using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizPositionController : MonoBehaviour
{
    public static QuizPositionController Instance;
    
    public GameObject quizUIGO;
    public QuizUIController quizUIController;
    
    public List<Transform> positionList = new List<Transform>();
    
    private int _nextIndex = 0;

    void Awake()
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
    
    public void GetQuiz()
    {
        GameManager.Instance.activeRayCast = false;
        quizUIGO.SetActive(true);
        quizUIController.GetQuiz();
    }
    
    public void QuitQuiz()
    {
        GameManager.Instance.activeRayCast = true;
        quizUIGO.SetActive(false);
        ActiveNextPosition();
    }
}
