using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public struct PostInfo
{
    public int userId;
    public int id;
    public string title;
    public string body;
}

[Serializable]
public struct Leaderboard
{
    public string clearTime;
    public string nickName;
    public string score;
}

public struct LeaderboardList
{
    public List<Leaderboard> data;
}

[Serializable]
public struct Choises
{
    public string choiseOne;
    public string choiseTwo;
    public string choiseThree;
    public string choiseFour;
}

[Serializable]
public struct Quiz
{
    public string quiz;
    public Choises choises;
    public int answerNum;
    public string description;
}

[Serializable]
public struct QuizArray
{
    public List<Quiz> data;

}

[Serializable]
public struct PostInfoArray
{
    public List<PostInfo> data;

}



public class HttpInfo
{
    public string url = "";
    public Action<DownloadHandler> OnComplete;
    internal string body;
    internal string contextType;
}

public class HttpManager : Singleton<HttpManager>
{
    /*
    private static HttpManager instance;

    public static HttpManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject();
            go.name = "HttpManager";
            go.AddComponent<HttpManager>();
        }

        return instance;
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    */

    #region 텍스트 요청

    public IEnumerator Get(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(info.url))
        {
            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }

    public IEnumerator Post(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, info.body, info.contextType))
        {
            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }

    #endregion

    #region 파일 요청 

    public IEnumerator UploadFileByFormData(HttpInfo info)
    {
        // 파일의 위치 
        // 파일을 byte 배열로 읽어 오자.
        byte[] data = File.ReadAllBytes(info.body);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormFileSection("file", data, "iamge.jpg", info.contextType));

        using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, formData))
        {
            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }

    public IEnumerator UploadFileByByte(HttpInfo info)
    {
        // 파일의 위치 
        // 파일을 byte 배열로 읽어 오자.
        byte[] data = File.ReadAllBytes(info.body);

        using (UnityWebRequest webRequest = new UnityWebRequest(info.url, "POST"))
        {
            // 업로드 하는 데이터
            webRequest.uploadHandler = new UploadHandlerRaw(data);
            webRequest.uploadHandler.contentType = info.contextType;

            //응답 받는 데이터 공간
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }

    #endregion

    public IEnumerator DownloadImage(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(info.url))
        {
            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }

    public IEnumerator DownloadAudio(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(info.url, AudioType.WAV))
        {
            yield return webRequest.SendWebRequest();

            //DownloadHandlerAudioClip handler = webRequest.downloadHandler as DownloadHandlerAudioClip;
            //handler.audioClip;

            DoneRequest(webRequest, info);
        }
    }

    public void DoneRequest(UnityWebRequest webRequest, HttpInfo info)
    {
        if (webRequest.result == UnityWebRequest.Result.Success)
        {

            if (info.OnComplete != null)
            {
                info.OnComplete(webRequest.downloadHandler);
            }

            Debug.Log("요청 성공");
        }
        else
        {
            Debug.LogError("에러 : " + webRequest.error);
        }
    }

}
