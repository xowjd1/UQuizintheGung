using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject quizGO;
    
    private void Update()
    {
        if (!GameManager.Instance.activeAutoPlayer)
        {
            player.SetActive(true);
            quizGO.SetActive(false);
        }
    }
}
