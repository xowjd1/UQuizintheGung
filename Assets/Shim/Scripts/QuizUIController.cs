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
        stageText.text = "�������� " + GameManager.Instance.stage.ToString() + "-" + GameManager.Instance.level.ToString();

        answerNum = quizUIText.answerNum;


    }

    public void AnswerFinish(int number)
    {

        Debug.Log("��ȣ : " + number + ", ���� :" + answerNum);

        if (number == answerNum)
        {
            Debug.Log("���� �Դϴ�.");
            PopUpAnswer(1);

        }
        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�.");
            PopUpAnswer(0);
        }
    }



    private void PopUpAnswer(int answer)
    {
        if(answer == 1)
            StartCoroutine(PopUpAnswerPlay("�����Դϴ�!!", answer));
        else
            StartCoroutine(PopUpAnswerPlay("Ʋ�Ƚ��ϴ�!!", answer));
    }

    private IEnumerator PopUpAnswerPlay(string text, int answer)
    {
        successFailurePanel.gameObject.SetActive(true);
        successFailureText.text = text.ToString(); 
        yield return new WaitForSeconds(successFailurePanelTimer);

        if (answer == 1)
        { 
            successFailureText.text = "���� ������ ����˴ϴ�.";
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
            Debug.Log("���� ���������� �̵�");
        }
    }
}
