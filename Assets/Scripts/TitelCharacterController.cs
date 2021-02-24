using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitelCharacterController : MonoBehaviour
{
    Rigidbody m_rb;
    [SerializeField] float moveSpeed = 3f;
    float dir = 1;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.velocity = new Vector3(moveSpeed, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        dir *= -1;
        moveSpeed *= -1;
        transform.rotation = Quaternion.LookRotation(new Vector3(dir, 0, 0));
    }
}
