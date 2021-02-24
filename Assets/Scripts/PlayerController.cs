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

    [SerializeField] float m_movingSpeed = 5f;
    [SerializeField] float m_turnSpeed = 3f;

    Rigidbody m_rb;
    Animator m_anim;
    [SerializeField] GameObject m_attackCollider = null;
    [SerializeField] int m_life = 1;
    [SerializeField] int m_maxLife = 2;
    [SerializeField] Slider m_lifeGauge = null;
    EnemyDetector m_enemyDetector = null;
    public static PlayerController Instance { get; private set; }
    public bool m_damageFrag = false;

    AudioSource audioSource;
    [SerializeField] AudioClip m_attackSound;
    [SerializeField] AudioClip m_damageSound;
    [SerializeField] AudioClip m_dieSound;
    [SerializeField] AudioClip m_slashSound;

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
        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_anim.Play("Damaged");
            DOTween.To(() => m_lifeGauge.value,
                l =>
                {
                    m_lifeGauge.value = l;
                },
                (float)m_life / m_maxLife,
                1f);
        }
    }
    public void AttackSoundPlay()
    {
        audioSource.PlayOneShot(m_attackSound);
    }
    public void DamageSoundPlay()
    {
        audioSource.PlayOneShot(m_damageSound);
        //audioSource.PlayOneShot(m_slashSound);
    }
}
