using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverFadeOut : MonoBehaviour
{
    Animator anim;
    bool frag = false;
    GameObject gameoverPlayer;
    public static GameOverFadeOut Instance { get; private set; }
    void Awake()
    {
        Instance = this;
        frag = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gameoverPlayer = GameObject.FindGameObjectWithTag("Finish");
        if (gameoverPlayer && !frag)
        {
            anim.SetBool("SceneBool", true);
            frag = true;
        }
    }
    public void StartScene()
    {
        anim.Play("FadeinAnim");
        frag = false;
    }
    public void LordGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeFrag()
    {
        GameManager.gameStartFrag = true;
    }
}
