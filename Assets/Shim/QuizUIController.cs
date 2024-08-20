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
    public string description;
}


public class QuizUIController : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI titleText;
    
    public TextMeshProUGUI choiseOneText;
    public TextMeshProUGUI choiseTwoText;
    public TextMeshProUGUI choiseThreeText;
    public TextMeshProUGUI choiseFourText;
    public int answerNum;

    //public GameObject popup


    public void SetQuizData(QuizUIText quizUIText)
    {
        //stageText.text = quizUIText.stageText.ToString();
        titleText.text = quizUIText.titleText.ToString();
        choiseOneText.text = quizUIText.choiseOneText.ToString();
        choiseTwoText.text = quizUIText.choiseThreeText.ToString();
        choiseThreeText.text = quizUIText.choiseThreeText.ToString();
        choiseFourText.text = quizUIText.choiseFourText.ToString();
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
        
    }
}
