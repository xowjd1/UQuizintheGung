using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpTest : MonoBehaviour
{
    public QuizUIController controller;


    public QuizArray allQuiz;
    public Quiz quiz;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            HttpInfo info = new HttpInfo();
            //
            info.url = "http://192.168.1.44:8080/test/leaderboard"; //"https://jsonplaceholder.typicode.com/posts";
            info.OnComplete = downloadHandler => {
                print(downloadHandler.text);
                string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
                print(jsonData);
                LeaderboardList items = JsonUtility.FromJson<LeaderboardList>(jsonData);

                foreach (var item in items.data)
                {
                    Debug.Log(item.nickName);
                    Debug.Log(item.score);
                    Debug.Log(item.clearTime);
                }


            };
            StartCoroutine(HttpManager.GetInstance().Get(info));
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HttpInfo info = new HttpInfo();
            //
            info.url = "http://192.168.1.44:8080/test/test"; //"https://jsonplaceholder.typicode.com/posts";
            info.OnComplete = downloadHandler => {
                print(downloadHandler.text);
                string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
                print(jsonData);
                allQuiz = JsonUtility.FromJson<QuizArray>(jsonData);

                print(allQuiz.data.Count);

                foreach (var item in allQuiz.data)
                {
                    print("quiztitle" + item.quiz);

                    print("=============================");

                    print(item.choises.choiseOne);
                    print(item.choises.choiseTwo);
                    print(item.choises.choiseThree);
                    print(item.choises.choiseFour);

                    print("=============================");

                    print(item.answerNum);
                    print(item.description);


                }

                QuizUIText quizText = new QuizUIText();
                quizText.titleText = allQuiz.data[0].quiz;
                quizText.choiseOneText = allQuiz.data[0].choises.choiseOne;
                quizText.choiseTwoText = allQuiz.data[0].choises.choiseTwo;
                quizText.choiseThreeText = allQuiz.data[0].choises.choiseThree;
                quizText.choiseFourText = allQuiz.data[0].choises.choiseFour;
                quizText.answerNum = allQuiz.data[0].answerNum;
                controller.SetQuizData(quizText);

            };
            StartCoroutine(HttpManager.GetInstance().Get(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HttpInfo info = new HttpInfo();
            info.url = "https://ssl.pstatic.net/melona/libs/1506/1506615/7dc09761f845afcf5f51_20240816112759137.jpg";
            info.OnComplete = downloadHandler => {
                File.WriteAllBytes(Application.dataPath + "/image2.jpg", downloadHandler.data);
                //print(downloadHandler.text);
            };
            StartCoroutine(HttpManager.GetInstance().Get(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HttpInfo info = new HttpInfo();
            UserInfo userInfo = new UserInfo();
            userInfo.name = "��Ÿ����";
            userInfo.age = 3;
            userInfo.height = 185f;
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/json";
            info.body = JsonUtility.ToJson(userInfo);
            info.contextType = "application/json";

            info.OnComplete = downloadHandler => {
                print(downloadHandler.text);
            };

            StartCoroutine(HttpManager.GetInstance().Post(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HttpInfo info = new HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/file";
            info.body = "C:\\Users\\hotan\\Downloads\\Charge.png";
            info.contextType = "multipart/form-data";
            info.OnComplete = downloadHandler => {
                File.WriteAllBytes(Application.dataPath + "/test.png", downloadHandler.data);
                //print(downloadHandler.text);
            };

            StartCoroutine(HttpManager.GetInstance().UploadFileByFormData(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            HttpInfo info = new HttpInfo();
            info.url = "http://mtvs.helloworldlabs.kr:7771/api/byte";
            info.contextType = "image/png";
            info.body = "C:\\Users\\hotan\\Downloads\\Charge.png";
            info.OnComplete = downloadHandler => {
                File.WriteAllBytes(Application.dataPath + "/test2.png", downloadHandler.data);
            };

            StartCoroutine(HttpManager.GetInstance().UploadFileByByte(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            HttpInfo info = new HttpInfo();
            info.url = "https://ssl.pstatic.net/melona/libs/1506/1506331/059733bdf9e9e6dc85ce_20240813151029214.jpg";
            //info.contextType = "image/png";
            //info.body = "C:\\Users\\hotan\\Downloads\\Charge.png";
            info.OnComplete = downloadHandler => {


                // �ٿ�ε� �� �����͸� Texture2D�� ��ȯ
                DownloadHandlerTexture handler = downloadHandler as DownloadHandlerTexture;
                Texture2D texture = handler.texture;

                // texture �� �̿��ؼ� Sprite�� ��ȯ
                Sprite sprite =
                Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                Image image = GameObject.Find("Image").GetComponent<Image>();
                image.sprite = sprite;
            };

            StartCoroutine(HttpManager.GetInstance().DownloadImage(info));
        }
    }
}

[Serializable]
public struct UserInfo
{
    public string name;
    public int age;
    public float height;
}