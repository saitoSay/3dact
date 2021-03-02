using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] GameObject m_text;
    float m_timer = 0;
    private void Awake()
    {
        m_text.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > 3)
        {
            m_text.SetActive(true);
            if (Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
