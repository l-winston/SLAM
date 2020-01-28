using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float lin_speed = 0.1f;
    public float ang_speed = 1;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float ang = rigidbody.transform.eulerAngles.z;
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos += lin_speed * new Vector3(Mathf.Cos(Mathf.Deg2Rad * ang), Mathf.Sin(Mathf.Deg2Rad * ang), 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pos += -lin_speed * new Vector3(Mathf.Cos(Mathf.Deg2Rad * ang), Mathf.Sin(Mathf.Deg2Rad * ang), 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            ang += ang_speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            ang += -ang_speed;
        }

        rigidbody.MovePosition(pos);

        rigidbody.MoveRotation(ang);
    }
}
