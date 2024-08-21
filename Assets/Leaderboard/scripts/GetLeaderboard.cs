using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using System;
using System.Globalization;
using System.Linq;
using static System.Net.WebRequestMethods;
//using System.IO;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

public class GetLeaderboard : MonoBehaviour
{
    public TMP_Text[] names;
    public TMP_Text[] scores;
    public JsonList myData;

    //string url = "http://192.168.1.44:8080/test/leaderboard";
    string url = "http://192.168.1.44:8080/api/leaderboard?nickname=fewaf&score=241";
    //string url = "http://192.168.1.44:8080/api/dbcheck";

    void Start()
    {
        GetServerData(url);

        //TestJson();
    }

    void Update()
    {

    }

    void TestJson()
    {
        //string path = Application.dataPath + "/test.json";
        //string json =  File.ReadAllText(path);
        //print(json);

        // NewtonSoft Parse 방식
        //JArray jsonArr = JArray.Parse(json);
        //foreach(JObject item  in jsonArr)
        //{
        //    print(item.GetValue("clearTime"));
        //    print(item.GetValue("score"));
        //    print(item.GetValue("nickName"));
        //}
        //newTestList = JsonUtility.FromJson<JsonList>(myJson);

        // Json 배열에 키를 강제로 추가하는 방식
        //string myJson = "{\"serverDataList\": " + json + "}";
        //newTestList = JsonUtility.FromJson<JsonData>(json);

    }

    public void GetServerData(string url)
    {
        StartCoroutine(GetServerDataProcess(url));
    }

    IEnumerator GetServerDataProcess(string url)
    {

        #region 선생님
        //UnityWebRequest request = new UnityWebRequest(url, "GET");
        //request.downloadHandler = new DownloadHandlerBuffer();

        //yield return request.SendWebRequest();

        //if (request.result == UnityWebRequest.Result.Success)
        //{
        //    string jsonData = request.downloadHandler.text;

        //    string myJson = "{\"serverDataList\": " + jsonData + "}";
        //    myData = JsonUtility.FromJson<JsonList>(myJson);


        //    for (int i = 0; i < names.Length; i++)
        //    {
        //        names[i].text = myData.serverDataList[i].nickName;
        //        scores[i].text = myData.serverDataList[i].score.ToString();
        //    }
        //}
        //else
        //{
        //    print(request.error);
        //}
        #endregion


        UnityWebRequest request = new UnityWebRequest(url, "GET");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonData = request.downloadHandler.text;

            string myJson = "{\"serverDataList\": " + jsonData + "}";
            myData = JsonUtility.FromJson<JsonList>(myJson);

            JsonData[] jsonDataArray = myData.serverDataList.ToArray();

            for (int i = 0; i < jsonDataArray.Length - 1; i++)
            {
                for (int j = i; j < jsonDataArray.Length; j++)
                {
                    if (myData.serverDataList[i].score < myData.serverDataList[j].score)
                    {
                        JsonData temp;
                        temp = jsonDataArray[i];
                        jsonDataArray[i] = jsonDataArray[j];
                        jsonDataArray[j] = temp;
                    }
                }
            }
            myData.serverDataList = jsonDataArray.ToList();

            for (int i = 0; i < names.Length; i++)
            {
                names[i].text = myData.serverDataList[i].nickName;
                scores[i].text = myData.serverDataList[i].score.ToString();

            }

        }
        else
        {
            print(request.error);
        }



        #region 
        //using (UnityWebRequest request = UnityWebRequest.Get(url))
        //{
        //    yield return request.SendWebRequest();

        //    //string jsonData = request.downloadHandler.text;

        //    //print(jsonData);

        //    string jsonData = "{ \"serverDataList\" : " + request.downloadHandler.text + "}";
        //    print(jsonData);
        //    JsonList myData = JsonUtility.FromJson<JsonList>(jsonData);


        //    print(myData.serverDataList.Count);


        //    foreach (var item in myData.serverDataList)
        //    {
        //        Debug.Log(item);
        //    }
        //}
        #endregion


        //UnityWebRequest request = new UnityWebRequest(url, "GET");

        //yield return request.SendWebRequest();

        //string jsonData = request.downloadHandler.text;
        //JsonList myData = JsonUtility.FromJson<JsonList>(jsonData);

        //for(int i = 0; i < myData.serverDataList.Count; i++)
        //{
        //    names[i].text = myData.serverDataList[i].nickName;
        //    scores[i].text = myData.serverDataList[i].score.ToString();
        //}

    }

}


[System.Serializable]
public struct JsonData
{
    public string clearTime;
    public int score;
    public string nickName;
}

[System.Serializable]
public class JsonList
{
    public List<JsonData> serverDataList;
}