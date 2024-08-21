using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;

public class GetLeaderboard : MonoBehaviour
{
    public TMP_Text[] names;
    public TMP_Text[] scores;

    string url = "http://192.168.1.44:8080/test/leaderboard";

    void Start()
    {
        GetServerData(url);
    }

    void Update()
    {
        
    }

    public void GetServerData(string url)
    {
        StartCoroutine(GetServerDataProcess(url));
    }

    IEnumerator GetServerDataProcess(string url)
    {
        /*
        UnityWebRequest request = new UnityWebRequest(url, "GET");

        yield return request.SendWebRequest();

        string jsonData = request.downloadHandler.text;
        JsonList myData = JsonUtility.FromJson<JsonList>(jsonData);

        for(int i = 0; i < myData.serverDataList.Count; i++)
        {
            names[i].text = myData.serverDataList[i].nickName;
            scores[i].text = myData.serverDataList[i].score.ToString();
        }
        */

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        { 
            yield return request.SendWebRequest();

            //string jsonData = request.downloadHandler.text;

            //print(jsonData);

            string jsonData = "{ \"serverDataList\" : " + request.downloadHandler.text + "}";
            print(jsonData);
            JsonList myData = JsonUtility.FromJson<
                .>(jsonData);

        
            print(myData.serverDataList.Count);


            foreach (var item in myData.serverDataList)
            {
                Debug.Log(item);
            }
        }


    }
}

public struct JsonData
{
    public string clearTime;
    public string nickName;
    public int score;
}

public struct JsonList
{
    public List<JsonData> serverDataList;
}