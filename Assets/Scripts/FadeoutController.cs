using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeoutController : MonoBehaviour
{
    Animator anim;
    bool frag = false;
    void Start()
    {
        frag = false;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.anyKeyDown && !frag)
        {
            anim.SetBool("Trigger", true);
            frag = true;
        }
    }
}
