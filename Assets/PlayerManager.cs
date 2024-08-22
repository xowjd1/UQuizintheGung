using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject mainCamera;
    public CamRotate camRotate;
    public GameObject player;
    public GameObject quizGO;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // todo 리더보드 끄면 quiz와 관련된 모든 오브젝트 비활성화 해야함  
            GameManager.Instance.activeAutoPlayer = false;
        }

        if (!GameManager.Instance.activeAutoPlayer)
        {
            camRotate.enabled = true;
            player.SetActive(true);
            mainCamera.GetComponent<CamFollow>().target = player.transform.GetChild(0);

            quizGO.SetActive(false);

            GameManager.Instance.activeAutoPlayer = true;
        }
    }
}
