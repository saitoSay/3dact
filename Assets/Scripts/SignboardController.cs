using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignboardController : MonoBehaviour
{
    public static int s_boradFrag;
    void Start()
    {
        s_boradFrag = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            s_boradFrag = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            s_boradFrag = 2;
        }
    }
}
