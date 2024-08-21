using UnityEngine;

public class QuizPos : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // quizUI 활성화
            QuizPositionController.Instance.GetQuiz();
            
            // quizPosition 비활성화 
            gameObject.SetActive(false);
        }
    }
}
