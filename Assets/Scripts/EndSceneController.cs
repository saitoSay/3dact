using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneController : MonoBehaviour
{
    [SerializeField]float m_timer = 0;
    
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
            this.gameObject.SetActive(true);
            if (Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
