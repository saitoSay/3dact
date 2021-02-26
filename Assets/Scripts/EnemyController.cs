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
    [SerializeField] float m_movePower = 10f;
    [SerializeField] float m_maxSpeed = 2f;

    [SerializeField] float m_targetRange = 4f;
    [SerializeField] float m_attackRange = 2f;
    [SerializeField] float m_detectInterval = 1f;
    [SerializeField] float m_turnSpeed = 3f;

    Animator m_anim;
    GameObject m_player = null;
    Rigidbody m_rb;
    public bool m_damageFrag = false;
    bool m_dieFrag = false;

    AudioSource audioSource;
    [SerializeField] AudioClip m_damageSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_life = m_maxLife;
        m_lifeGauge.gameObject.SetActive(false);
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }
    void Update()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player)
        {
            float distance = Vector3.Distance(this.transform.position, m_player.transform.position);
            if (m_damageFrag)
            {
                m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
            }
            else if (distance < m_targetRange && !m_dieFrag)
            {
                m_player = GameObject.FindGameObjectWithTag("Player");
                Vector3 dir = m_player.transform.position - this.transform.position;
                dir.y = 0;
                this.transform.forward = dir;

                if (distance < m_attackRange)
                {
                    m_anim.SetTrigger("AttackFrag");
                    m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
                }
                else if (m_rb.velocity.magnitude < m_maxSpeed)
                {
                    m_rb.AddForce(this.transform.forward * m_movePower);
                }
            }
            else
            {
                m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
            }
        }
        if (m_player)
        {
            if (m_targetRange < Vector3.Distance(this.transform.position, m_player.transform.position))
            {
                m_player = null;
            }
        }
    }
    public void ResetAnim()
    {
        m_damageFrag = false;
        m_anim.ResetTrigger("AttackFrag");
    }
    public void Damage()
    {
        audioSource.PlayOneShot(m_damageSound);
        m_damageFrag = true;
        m_lifeGauge.gameObject.SetActive(true);
        m_life -= 3;
        if (m_life <= 0)
        {
            m_dieFrag = true;
            m_anim.Play("Die");
            DOTween.To(() => m_lifeGauge.value,
                l =>
                {
                    m_lifeGauge.value = l;
                },
                (float)m_life / m_maxLife,
                1f);
            GameManager.s_enemyCount--;
        }
        else
        {
            m_anim.Play("GetHit");
            DOTween.To(() => m_lifeGauge.value, 
                l =>
                {
                    m_lifeGauge.value = l;
                },
                (float)m_life / m_maxLife,
                1f);
        }
    }
    public void AnimationDestroy()
    {
        Destroy(this.gameObject);
    }
}
