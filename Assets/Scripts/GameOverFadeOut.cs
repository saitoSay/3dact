using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFadeOut : MonoBehaviour
{
    Animator anim;
    bool frag = false;
    GameObject gameoverPlayer;
    // Start is called before the first frame update
    void Start()
    {
        frag = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gameoverPlayer = GameObject.FindGameObjectWithTag("Finish");
        if (gameoverPlayer && !frag)
        {
            anim.SetBool("Trigger", true);
            frag = true;
        }
    }
}
