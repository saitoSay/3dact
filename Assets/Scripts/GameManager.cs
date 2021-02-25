using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static bool gameStartFrag = true;
    [SerializeField] GameObject m_playerPrefab;
    [SerializeField] GameObject m_enemyPrefab;
    [SerializeField] GameObject m_playerPos;
    [SerializeField] GameObject[] m_enemysPos;

    
    void Awake()
    {
        if (gameStartFrag)
        {
            ReStart();
        }
    }
    public void ReStart()
    {
        //Instantiate(m_playerPrefab, m_playerPos.transform);
        for (int i = 0; i < m_enemysPos.Length; i++)
        {
            Instantiate(m_enemyPrefab, m_enemysPos[i].transform);
        }
        gameStartFrag = false;
        GameOverFadeOut.Instance.StartScene();
    }
}
