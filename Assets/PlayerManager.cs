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
            // todo �������� ���� quiz�� ���õ� ��� ������Ʈ ��Ȱ��ȭ �ؾ���  
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
