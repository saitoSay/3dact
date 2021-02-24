using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeoutController : MonoBehaviour
{
    Animator anim;
    bool frag = false;
    // Start is called before the first frame update
    void Start()
    {
        frag = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !frag)
        {
            anim.SetBool("Trigger", true);
            frag = true;
        }
    }
}
