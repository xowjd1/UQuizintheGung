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

[Serializable]
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

    #region �ؽ�Ʈ ��û

    public IEnumerator Get(HttpInfo info)
    {
        using var webRequest = UnityWebRequest.Get(info.url);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();
        
        Debug.Log(webRequest.result);
        Debug.Log(webRequest.downloadHandler.text);
    }

    
    private IEnumerator SendTest(string url)
    {
        using var webRequest = UnityWebRequest.Get(url);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
    }
    
    public IEnumerator Post(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(info.url, info.body))
        {
            yield return webRequest.SendWebRequest();
            DoneRequest(webRequest, info);
        }
    }

    #endregion

    #region ���� ��û 

    public IEnumerator UploadFileByFormData(HttpInfo info)
    {
        // ������ ��ġ 
        // ������ byte �迭�� �о� ����.
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
        // ������ ��ġ 
        // ������ byte �迭�� �о� ����.
        byte[] data = File.ReadAllBytes(info.body);

        using (UnityWebRequest webRequest = new UnityWebRequest(info.url, "POST"))
        {
            // ���ε� �ϴ� ������
            webRequest.uploadHandler = new UploadHandlerRaw(data);
            webRequest.uploadHandler.contentType = info.contextType;

            //���� �޴� ������ ����
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
        Debug.Log(webRequest.downloadHandler.text);
        
        if (webRequest.result == UnityWebRequest.Result.Success)
        {

            if (info.OnComplete != null)
            {
                info.OnComplete(webRequest.downloadHandler);
            }

            Debug.Log("��û ����");
        }
        else
        {
            Debug.LogError("���� : " + webRequest.error);
        }
    }

}
