using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    /// <summary>索敵範囲</summary>
    [SerializeField] float m_targetRange = 4f;
    /// <summary>敵の検出を行う間隔（単位: 秒）</summary>
    [SerializeField] float m_detectInterval = 1f;
    /// <summary>ロックオンしているオブジェクト</summary>
    GameObject m_target = null;
    public static bool m_lockonFrag = false;
    float m_timer;

    GameObject[] images;
    private void Start()
    {
        images = GameObject.FindGameObjectsWithTag("Image");
        foreach (var item in images)
        {
            item.SetActive(false);
        }
    }

    /// <summary>
    /// ロックオンしている敵を取得する
    /// </summary>
    public GameObject Target
    {
        get { return m_target; }
    }

    void Update()
    {
        //ロックオン切り替え
        if (Input.GetButtonDown("Fire2"))
        {
            if (Target && !m_lockonFrag)
            {
                m_lockonFrag = true;
                GameObject canvas = Target.transform.Find("Canvas").gameObject;
                GameObject lockonIcon = canvas.transform.Find("Image").gameObject;
                images = GameObject.FindGameObjectsWithTag("Image");
                foreach (var item in images)
                {
                    item.SetActive(false);
                }
                lockonIcon.SetActive(true);
            }
            else
            {
                m_lockonFrag = false;
                images = GameObject.FindGameObjectsWithTag("Image");
                foreach (var item in images)
                {
                    item.SetActive(false);
                }
            }
        }
        if (!Target)
        {
            m_lockonFrag = false;
        }


        m_timer += Time.deltaTime;

        // 一定間隔で検出を行う
        if (m_timer > m_detectInterval)
        {
            m_timer = 0;

            // シーン内の敵を全て取得する
            GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemyArray)
            {
                float distance = Vector3.Distance(this.transform.position, enemy.transform.position);

                if (distance < m_targetRange)
                {
                    if (m_target == null || distance < Vector3.Distance(this.transform.position, m_target.transform.position) && !m_lockonFrag)
                    {
                        m_target = enemy;
                    }
                }
            }
        }

        // ロックオンしているターゲットが索敵範囲外に出たらロックオンをやめる
        if (m_target)
        {
            if (m_targetRange < Vector3.Distance(this.transform.position, m_target.transform.position))
            {
                m_target = null;
                images = GameObject.FindGameObjectsWithTag("Image");
                foreach (var item in images)
                {
                    item.SetActive(false);
                }
            }
        }
    }
}
