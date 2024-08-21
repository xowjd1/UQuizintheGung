using System.Collections;
using UnityEngine;

public class QuizPos : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Wait());

            // quizUI 활성화
            QuizPositionController.Instance.GetQuiz();
            
            // quizPosition 비활성화 
            gameObject.SetActive(false);
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }

}
