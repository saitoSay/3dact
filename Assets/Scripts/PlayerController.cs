using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// プレイヤー
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary>回転速度</summary>
    [SerializeField] float m_turnSpeed = 3f;
    Rigidbody m_rb;
    Animator m_anim;
    /// <summary>攻撃時の当たり判定</summary>
    [SerializeField] GameObject m_attackCollider = null;
    /// <summary>現在の体力</summary>
    [SerializeField] int m_life = 1;
    /// <summary>体力の最大値</summary>
    [SerializeField] int m_maxLife = 2;
    [SerializeField] Slider m_lifeGauge = null;
    EnemyDetector m_enemyDetector = null;

    /// <summary>プレイヤーの情報</summary>
    public static PlayerController Instance { get; private set; }
    public bool m_damageFrag = false;
    public bool m_gameoverFrag = false;

    AudioSource audioSource;
    [SerializeField] AudioClip m_attackSound;
    [SerializeField] AudioClip m_damageSound;
    [SerializeField] AudioClip m_dieSound;
    [SerializeField] AudioClip m_slashSound;

    [SerializeField] GameObject m_gameoverPrefab;

    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_enemyDetector = GetComponent<EnemyDetector>();
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        
        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        if (Input.GetButtonDown("Fire1"))
        {
            if (EnemyDetector.m_lockonFrag)
            {
                this.transform.LookAt(m_enemyDetector.Target.transform.position);
            }
            m_anim.SetTrigger("fire");
        }
        else
        {
            m_anim.ResetTrigger("fire");
        }
        
        if (dir == Vector3.zero)
        {
            m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
        }
        else
        {
            dir = Camera.main.transform.TransformDirection(dir); 
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);  // Slerp を使うのがポイント

            Vector3 velo = dir.normalized * m_movingSpeed; 
            m_rb.velocity = velo;
        }
    }
    void LateUpdate()
    {
        Vector3 horizontalVelocity = m_rb.velocity;
        horizontalVelocity.y = 0;
        m_anim.SetFloat("Speed", horizontalVelocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAttack")
        {
            Damage();
        }
    }

    public void BiginAttack()
    {
        m_attackCollider.SetActive(true);
    }
    public void EndAttack()
    {
        m_attackCollider.SetActive(false);
    }

    public void Damage()
    {
        m_life -= 1;
        m_anim.Play("Damaged");
        DOTween.To(() => m_lifeGauge.value,
            l =>
            {
                if (m_life <= 0)
                {
                    this.gameObject.SetActive(false);
                    if (!m_gameoverFrag)
                    {
                        Instantiate(m_gameoverPrefab, this.gameObject.transform.position, this.transform.rotation);
                        m_gameoverFrag = true;
                    }
                }
                m_lifeGauge.value = l;
            },
            (float)m_life / m_maxLife,
            1f);

    }
    public void AttackSoundPlay()
    {
        audioSource.PlayOneShot(m_attackSound);
    }
    public void DamageSoundPlay()
    {
        audioSource.PlayOneShot(m_damageSound);
    }
}
