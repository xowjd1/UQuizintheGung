using System;
using UnityEngine;

public class QuizPosition : MonoBehaviour
{
    private void Start()
    {
        transform.Find("Portal").gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("QuizPosition.OnTriggerEnter() hit : " + other.transform.name);

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player hit");
        }
    }
}
