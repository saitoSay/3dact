using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>プレイヤーアイコンの変更用コンポーネント</summary>
public class PlayerIconController : MonoBehaviour
{
    private Image m_playerIcon = null;
    [SerializeField] Sprite[] m_sprites = null;
    void Start()
    {
        m_playerIcon = GetComponent<Image>();
    }
    void Update()
    {
        if (PlayerController.Instance.m_damageFrag || PlayerController.Instance.m_gameoverFrag)
        {
            m_playerIcon.sprite = m_sprites[1];
        }
        else
        {
            m_playerIcon.sprite = m_sprites[0];
        }
    }
}
