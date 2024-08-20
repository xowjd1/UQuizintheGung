using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        // 카메라를 캐릭터의 머리통에 위치시킴 
        transform.position = target.position;
    }
}
