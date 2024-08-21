using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject quizUIGO;
    
    private float _moveSpeed = 2f;
    private bool _shouldMove = false;
    private int _layerMask;

    
    void Start()
    {
        _layerMask = LayerMask.GetMask("portal");

        // QuizUI 비활성화
        quizUIGO.SetActive(false);
        QuizPositionController.Instance.ActiveNextPosition();
    }

    void Update()
    {
        if (GameManager.Instance.activeRayCast) GetMouseButtonDown();
        MovePlayer();
    }
    
    // 다음 포지션 클릭 시 포지현으로 자동 이동
    void GetMouseButtonDown()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            //Debug.Log("마우스 좌클릭 ");
            
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;


            //Debug.Log("Raycast hit : " + ray);


            // QuizPosition hit
            if (Physics.Raycast(ray, out hit, 1000f, _layerMask))
            {
              
                //Debug.Log("Raycast hit : " + hit.transform.name);
                StartMoving();  
            }
        }
    }
    
    void MovePlayer()
    {
        // 플레이어 이동 
        if (_shouldMove)
        {
            Vector3 targetPosition = QuizPositionController.Instance.GetNextPosition();
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, _moveSpeed * Time.deltaTime);

            // 목표 위치에 거의 도달했는지 확인
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                _shouldMove = false; // 목표 위치에 도달하면 이동 중지
                audioSource.Stop();
                QuizPositionController.Instance.SetNextPosition();
            }
        }
    }

    void StartMoving()
    {
        _shouldMove = true;
        // float journeyLength = Vector3.Distance(transform.position, positionList[_nextIndex].position);
        // // 오디오 클립 길이 
        // float clipLength = audioSource.clip.length;
        // // 예상 이동 시간
        // float travelTime = journeyLength / _moveSpeed;
        // // 오디오 클립의 재생 속도를 이동 시간에 맞추기
        // audioSource.pitch = clipLength / travelTime;
        audioSource.Play();
    }
}
