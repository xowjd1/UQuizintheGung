using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float gravity = -20f;
    public float jumpPower = 10f;
    public bool isJumping = false;
    
    private CharacterController cc;
    // 수직 속력 변수 
    private float yVelocity;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        // 키보드 입력 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 dir = new Vector3(h, 0f, v);
        dir.Normalize();
        dir = Camera.main.transform.TransformDirection(dir);


        if (cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
                yVelocity = 0f;
            }
        }

        if (Input.GetButtonDown("Jump") && !isJumping) 
        {
            yVelocity = jumpPower; 
            isJumping = true; // 무한 점프 막기 
        }
        
        // y속도 
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        

        // transform.position += moveSpeed * Time.deltaTime * dir;
        
        // Transform으로 움직이는 걸 CharacterController로 움직이도록 변경 
        cc.Move(moveSpeed * Time.deltaTime * dir);
        
    }
} 
