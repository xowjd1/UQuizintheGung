using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class QuizUIText
{ 
    public string stageText;
    public string titleText;

    public string choiseOneText;
    public string choiseTwoText;
    public string choiseThreeText;
    public string choiseFourText;

    public int answerNum;
    public string descriptionText;
}


public class QuizUIController : MonoBehaviour
{

    [Header("다음 레벨")]
    public int maxNextLevel = 2;

    [Header("퀴즈 UI 스테이지")]
    public TextMeshProUGUI stageText;

    [Header("퀴즈 내용")]
    public TextMeshProUGUI titleText;

    [Header("퀴즈 UI 사지선다")]
    public TextMeshProUGUI choiseOneText;
    public TextMeshProUGUI choiseTwoText;
    public TextMeshProUGUI choiseThreeText;
    public TextMeshProUGUI choiseFourText;

    [Header("퀴즈 UI 정답")]
    public int answerNum;
    
    [Header("퀴즈 UI 해설")]
    public TextMeshProUGUI descriptionText;

    [Header("퀴즈 UI 정답팝업")]
    public GameObject successFailurePanel;

    [Header("퀴즈 UI 정답팝업 텍스트")]
    public TextMeshProUGUI successFailureText;

    [Header("퀴즈 UI 정답팝업 대기시간")]
    public float successFailurePanelTimer = 2f;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
        }
    }

    private static int GetCurrentQuizIndex()
    {
        return GameManager.Instance.stage + GameManager.Instance.level - 2;
    }


    // TODO 스테이지 넘버로 문제 가져오기
    public void SetQuizData(QuizUIText quizUIText)
    {
        //stageText.text = quizUIText.stageText.ToString();
        titleText.text = quizUIText.titleText.ToString();
        choiseOneText.text = quizUIText.choiseOneText.ToString();
        choiseTwoText.text = quizUIText.choiseThreeText.ToString();
        choiseThreeText.text = quizUIText.choiseThreeText.ToString();
        choiseFourText.text = quizUIText.choiseFourText.ToString();
        descriptionText.text = quizUIText.descriptionText.ToString();
        stageText.text = "스테이지 " + GameManager.Instance.stage.ToString() + "-" + GameManager.Instance.level.ToString();

        answerNum = quizUIText.answerNum;


    }

    public void AnswerFinish(int number)
    {

        Debug.Log("번호 : " + number + ", 정답 :" + answerNum);

        if (number == answerNum)
        {
            Debug.Log("정답 입니다.");
            PopUpAnswer(1);

        }
        else
        {
            Debug.Log("틀렸습니다.");
            PopUpAnswer(0);
        }
    }



    private void PopUpAnswer(int answer)
    {
        if(answer == 1)
            StartCoroutine(PopUpAnswerPlay("정답입니다!!", answer));
        else
            StartCoroutine(PopUpAnswerPlay("틀렸습니다!!", answer));
    }

    private IEnumerator PopUpAnswerPlay(string text, int answer)
    {
        successFailurePanel.gameObject.SetActive(true);
        successFailureText.text = text.ToString(); 
        yield return new WaitForSeconds(successFailurePanelTimer);

        if (answer == 1)
        { 
            successFailureText.text = "다음 레벨로 변경됩니다.";
            NextLevel();
            yield return new WaitForSeconds(successFailurePanelTimer);
        
        }

        successFailurePanel.gameObject.SetActive(false);
    }

    private void NextLevel()
    {
        if (GameManager.Instance.level <= maxNextLevel)
        {
            GameManager.Instance.level++;
            QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
        }
        else
        {
            Debug.Log("다음 스테이지로 이동");
        }
    }
}
