using System;
using UnityEngine;

public class QuizPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("QuizPosition.OnTriggerEnter() hit : " + other.transform.name);

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("player hit");
        }
    }
}
