using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class SceneController : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip sound;
    Animator anim;
    bool frag = false;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        frag = false;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.anyKeyDown && !frag)
        {
            anim.SetBool("Trigger", true);
            frag = true;
            audioSource.PlayOneShot(sound);
        }
    }
    public void LordGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
