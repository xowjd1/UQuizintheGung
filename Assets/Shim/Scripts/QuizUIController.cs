using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    //public bool 
}


public class QuizUIController : MonoBehaviour
{
    public Slider bar;
    public float timeCounter;
    public float timer = 2f;

    [Header("���� ����")]
    public int maxNextLevel = 2;

    [Header("���� UI ��������")]
    public TextMeshProUGUI stageText;

    [Header("���� ����")]
    public TextMeshProUGUI titleText;

    [Header("���� UI ��������")]
    public TextMeshProUGUI choiseOneText;
    public TextMeshProUGUI choiseTwoText;
    public TextMeshProUGUI choiseThreeText;
    public TextMeshProUGUI choiseFourText;

    [Header("���� UI ����")]
    public int answerNum;
    
    [Header("���� UI �ؼ�")]
    public TextMeshProUGUI descriptionText;

    [Header("���� UI �����˾�")]
    public GameObject successFailurePanel;

    [Header("���� UI �����˾� �ؽ�Ʈ")]
    public TextMeshProUGUI successFailureText;

    [Header("���� UI �����˾� ���ð�")]
    public float successFailurePanelTimer = 1f;



    private void Update()
    {
        bar.value -= Time.deltaTime * .03f;

        if (bar.value <= 0)
        {
            bar.value = 1;

            GameManager.Instance.level++;
            PopUpAnswer(0);
        }



        // if (Input.GetKeyDown(KeyCode.L))
        // {
        //     QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
        // }
    }
    
    public void GetQuiz()
    {
        QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
    }

    private static int GetCurrentQuizIndex()
    {
        return GameManager.Instance.stage + GameManager.Instance.level - 2;
    }


    // TODO �������� �ѹ��� ���� ��������
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

        

        if (number == answerNum)
        {
            //Debug.Log("정답입니다!!");
            PopUpAnswer(1);

        }
        else
        {
            //Debug.Log("틀렸습니다!!");
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
            
            if (GameManager.Instance.level == maxNextLevel)
            {
                GameManager.Instance.levelCounter = true;
                GameManager.Instance.level = 1;
                
                GameManager.Instance.answerCounter++;
                GameManager.Instance.maxCounter--;

                yield return new WaitForSeconds(successFailurePanelTimer);
                successFailurePanel.gameObject.SetActive(false);
                StartCoroutine(StageMove());
                yield return null;
            }

            if (GameManager.Instance.level - 1 < maxNextLevel)
            { 
                successFailureText.text = "다음 레벨로 이동";
                yield return new WaitForSeconds(successFailurePanelTimer);
                successFailurePanel.gameObject.SetActive(false);

                GameManager.Instance.level++;

                GameManager.Instance.answerCounter++;
                GameManager.Instance.maxCounter--;

                QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
                yield return null;
            }

        }
        else
        {
            if (GameManager.Instance.level == maxNextLevel)
            {
                successFailureText.text = "다음 스테이지로 이동";

                GameManager.Instance.levelCounter = true;
                GameManager.Instance.level = 1;

                GameManager.Instance.answerCounter++;
                GameManager.Instance.maxCounter--;

                yield return new WaitForSeconds(successFailurePanelTimer);
                successFailurePanel.gameObject.SetActive(false);
                StartCoroutine(StageMove());
                yield return null;
            }

            if (GameManager.Instance.level - 1 < maxNextLevel)
            {
                successFailureText.text = "다음 문제로 이동";
                yield return new WaitForSeconds(successFailurePanelTimer);
                successFailurePanel.gameObject.SetActive(false);

                GameManager.Instance.level++;
                GameManager.Instance.answerCounter++;
                GameManager.Instance.maxCounter--;

                QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
                yield return null;
            }


        }
    }

    private void NextLevel()
    {
        Debug.Log(GameManager.Instance.level);
        Debug.Log("스테이지 : " + GameManager.Instance.stage + ", 정답 :" + GameManager.Instance.level);

        if (GameManager.Instance.level < maxNextLevel)
        {
            GameManager.Instance.level++;
            QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
        }

        /*
       if (GameManager.Instance.level == 3)
       {
           GameManager.Instance.open = true;
       }



       if (GameManager.Instance.level == maxNextLevel)
       {
           GameManager.Instance.stage++;
           StartCoroutine(StageMove());
       }


       else
       {
           //if()

           Debug.Log("다음 스테이지로 이동");
           QuizPositionController.Instance.QuitQuiz();
       }
       */
    }

    private IEnumerator StageMove()
    {
        Debug.Log("다음 스테이지로 이동");
        GameManager.Instance.stage++;
        QuizPositionController.Instance.QuitQuiz();
        successFailurePanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(successFailurePanelTimer);
    }
}
