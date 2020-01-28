using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 0.1f;

    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_pos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            new_pos += speed * Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            new_pos += speed * Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            new_pos += speed * Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            new_pos += speed * Vector3.right;
        }

        rb2d.MovePosition(new_pos);

    }
}
