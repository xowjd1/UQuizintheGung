using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

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
    public Vector3 vector;

    public Slider bar;
    public float timeCounter;
    public float timer = 2f;



    [Header("���� ����")]
    public int maxNextLevel = 2;

    [Header("���� UI ��������")]
    public TextMeshProUGUI stageText;

    [Header("���� ����")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI numberText;

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

    public GameObject panel;

    public GetLeaderboard leaderboard;


    private void Update()
    {
        bar.value -= Time.deltaTime * .03f;

        if (bar.value <= 0)
        {
            bar.value = 1;

            GameManager.Instance.level++;
            PopUpAnswer(0);
        }
    }
    
    public void GetQuiz()
    {
        QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
    }

    private static int GetCurrentQuizIndex()
    {
   
        //return GameManager.Instance.stage + GameManager.Instance.level - 2;
        return 8 - GameManager.Instance.maxCounter;
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

      


        if (GameManager.Instance.maxCounter == 3)
        {
            GameManager.Instance.open = true;
        }

        if (answer == 1)
        {
            
            if (GameManager.Instance.level == maxNextLevel)
            {
                

                GameManager.Instance.levelCounter = true;
                GameManager.Instance.level = 1;
                
                GameManager.Instance.answerCounter++;
                GameManager.Instance.maxCounter--;
                numberText.text = GameManager.Instance.maxCounter.ToString();

                if (GameManager.Instance.maxCounter == 0)
                {
                    Ending();
                }

                

                yield return new WaitForSeconds(successFailurePanelTimer);
                successFailurePanel.gameObject.SetActive(false);
                StartCoroutine(StageMove());
                bar.value = 1;

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
                numberText.text = GameManager.Instance.maxCounter.ToString();
                QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
                bar.value = 1;
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

                if (GameManager.Instance.maxCounter == 0)
                {
                    Ending();
                }

                numberText.text = GameManager.Instance.maxCounter.ToString();
                yield return new WaitForSeconds(successFailurePanelTimer);
                successFailurePanel.gameObject.SetActive(false);
                StartCoroutine(StageMove());
                bar.value = 1;
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
                numberText.text = GameManager.Instance.maxCounter.ToString();
                QuizData.Instance.GetQuizData(GetCurrentQuizIndex());
                bar.value = 1;
                yield return null;
            }
        }
    }

    private void Ending()
    {
        panel.gameObject.SetActive(false);
        bar.gameObject.SetActive(false);
        successFailurePanel.gameObject.SetActive(false);

        //todo 위치 이동
        // -3.5, 4.55, 64.29
        Vector3 targetPosition = new Vector3(-2.71f, 4.55f, 70.75f);

        // float distance = Vector3.Distance(GameManager.Instance.player.transform.position, targetPosition);
        
        Sequence sequence = DOTween.Sequence().SetAutoKill(false).Pause()
            .Append(GameManager.Instance.player.transform.DOMove(targetPosition, 1f, false))
            .Append(GameManager.Instance.player.transform.DORotate(new Vector3(0, -180, 0), 3));
            
        
        //todo 리더보드 호출 
        // leaderboard.GetServerData();

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
