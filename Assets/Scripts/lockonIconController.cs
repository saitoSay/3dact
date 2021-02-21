using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockonIconController : MonoBehaviour
{
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.Rotate(0, 0, -1f);
    }
}
