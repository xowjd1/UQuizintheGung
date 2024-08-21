using UnityEngine;

public class QuizPos : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            QuizPositionController.Instance.DeActivePosition();  
            // TODO QuizUI 활성화
            
            
        }
    }
}
