using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizData : Singleton<QuizData>
{
    public QuizUIController quizUI;
    public string quizDataUrl = "http://192.168.1.44:8080/test/test";

    public void GetQuizData(int level)
    {
        HttpInfo info = new HttpInfo();
        QuizArray quizArray;

        info.url = quizDataUrl;
        info.OnComplete = downloadHandler => {

            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            quizArray = JsonUtility.FromJson<QuizArray>(jsonData);

            //Debug.Log(quizArray);

            QuizUIText quizText = new QuizUIText();
            quizText.titleText = quizArray.data[level].quiz;
            quizText.choiseOneText = quizArray.data[level].choises.choiseOne;
            quizText.choiseTwoText = quizArray.data[level].choises.choiseTwo;
            quizText.choiseThreeText = quizArray.data[level].choises.choiseThree;
            quizText.choiseFourText = quizArray.data[level].choises.choiseFour;
            quizText.descriptionText = quizArray.data[level].description;
            quizText.answerNum = quizArray.data[level].answerNum;
            quizUI.SetQuizData(quizText);

        };
        StartCoroutine(HttpManager.Instance.Get(info));
    }

}
