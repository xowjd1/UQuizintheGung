using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoMove : MonoBehaviour
{
    public List<Transform> positionList = new List<Transform>();
    public AudioSource audioSource;
    
    private float _moveSpeed = 2f;
    private int _nextIndex = 1;
    private bool _shouldMove = false;
    
    void Start()
    {
        transform.position = positionList[0].position;
        
        //Test
        StartMoving();
    }

    void Update()
    {
        if (_shouldMove)
        {
            Vector3 targetPosition = positionList[_nextIndex].position;
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, _moveSpeed * Time.deltaTime);

            // 목표 위치에 거의 도달했는지 확인
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                _shouldMove = false; // 목표 위치에 도달하면 이동 중지
                audioSource.Stop();
                _nextIndex++;
            }
        }
    }

    // 이동을 시작하는 함수
    public void StartMoving()
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
