using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int m_life = 1;
    [SerializeField] int m_maxLife = 2;
    [SerializeField] Slider m_lifeGauge = null;

    [SerializeField] float m_targetRange = 4f;
    [SerializeField] float m_attackRange = 2f;
    [SerializeField] float m_detectInterval = 1f;
    [SerializeField] float m_turnSpeed = 3f;
    GameObject m_player = null;
    float m_timer;
    void Start()
    {
        m_life = m_maxLife;
        m_lifeGauge.gameObject.SetActive(false);
    }
    void Update()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        float distance = Vector3.Distance(this.transform.position, m_player.transform.position);

        if (distance < m_targetRange)
        {
            transform.LookAt(m_player.transform);
        }

        // ロックオンしているターゲットが索敵範囲外に出たらロックオンをやめる
        if (m_player)
        {
            if (m_targetRange < Vector3.Distance(this.transform.position, m_player.transform.position))
            {
                m_player = null;
            }
        }
    }



    public void Damage()
    {
        m_lifeGauge.gameObject.SetActive(true);
        m_life -= 3;
        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DOTween.To(() => m_lifeGauge.value, 
                l =>
                {
                    m_lifeGauge.value = l;
                },
                (float)m_life / m_maxLife,
                1f);
        }
    }
}
