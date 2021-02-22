using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// プレイヤーの挙動を制御するコンポーネント
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
    //[SerializeField] bool m_lockonFrag = false;
    EnemyDetector m_enemyDetector = null;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_enemyDetector = GetComponent<EnemyDetector>();
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        
        // 入力された方向を変換する
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
            // 方向の入力がニュートラルの時は、y 軸方向の速度を維持しながら xy 軸平面上は減速する
            m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
        }
        else
        {
            // カメラを基準に入力が上下=奥/手前, 左右=左右にキャラクターを向ける
            dir = Camera.main.transform.TransformDirection(dir);    // メインカメラを基準に入力方向のベクトルを変換する
            dir.y = 0;  // y 軸方向はゼロにして水平方向のベクトルにする

            // 入力方向に滑らかに回転させる
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);  // Slerp を使うのがポイント

            Vector3 velo = dir.normalized * m_movingSpeed; // 入力した方向に移動するvelo.y = m_rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            m_rb.velocity = velo;  // 計算した速度ベクトルをセットする
        }
    }
    void LateUpdate()
    {
        // 水平方向の速度を求めて Animator Controller のパラメーターに渡す
        Vector3 horizontalVelocity = m_rb.velocity;
        horizontalVelocity.y = 0;
        m_anim.SetFloat("Speed", horizontalVelocity.magnitude);
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
