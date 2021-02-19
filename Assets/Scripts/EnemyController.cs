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

    void Start()
    {
        m_life = m_maxLife;
        //m_lifeGauge.gameObject.SetActive(false);
    }
    public void Damage()
    {
        m_life -= 3;
        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // m_lifeGauge.value = (float)m_life / m_maxLife;
            DOTween.To(() => m_lifeGauge.value, 
                l =>
                {
                    if (m_life <= 0)
                    {
                        m_lifeGauge.value = l;
                    }
                    m_lifeGauge.value = l;
                },
                (float)m_life / m_maxLife,
                1f);

        }

    }
}
