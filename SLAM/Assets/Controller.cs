using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private float[] raycast_angle_degrees = { -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25 };

    private float lin_speed = 0.1f;
    private float ang_speed = 0.05f;
    private float sensor_offset = 0.333f;

    List<Data> datas = new List<Data>();

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 20 == 0)
        {
            CastRays();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float ang_radians = Mathf.Deg2Rad * rigidbody.transform.eulerAngles.z;
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            pos += lin_speed * new Vector3(Mathf.Cos(ang_radians), Mathf.Sin(ang_radians), 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pos += -lin_speed * new Vector3(Mathf.Cos(ang_radians), Mathf.Sin(ang_radians), 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            ang_radians += ang_speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            ang_radians += -ang_speed;
        }

        // "Try" to move to [pos] position
        rigidbody.MovePosition(pos);

        // "Try" to turn to [ang] degrees
        rigidbody.MoveRotation(Mathf.Rad2Deg * ang_radians);
    }

    void CastRays()
    {
        // Check for current angle
        Vector3 direction = transform.right;

        // Find sensor location
        Vector3 sensor_position = transform.position + sensor_offset * direction;

        Data data = Data.ScanPoints(sensor_position, direction, raycast_angle_degrees);
        datas.Add(data);

    }

    private void OnDrawGizmos()
    {
        foreach (Data data in datas)
        {
            for (int i = 0; i < data.points.Length; i++)
            {
                Gizmos.DrawSphere(data.points[i], 0.1f);
            }
        }
    }
}
