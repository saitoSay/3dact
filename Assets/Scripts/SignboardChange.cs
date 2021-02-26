using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignboardChange : MonoBehaviour
{
    Animator m_anim;
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (SignboardController.s_boradFrag == 1)
        {
            m_anim.Play("boradShow");
        }
        else if(SignboardController.s_boradFrag == 2)
        {
            m_anim.Play("boradClose");
        }
    }

}
