using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private float[] raycast_angle_degrees = { -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25 };

    private float lin_speed = 0.1f;
    private float ang_speed = 0.05f;
    private float offset = 0.333f;

    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        ProcessUserInput();

        CastRays();

    }

    void ProcessUserInput()
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
        float current_ang_radians = Mathf.Deg2Rad * transform.eulerAngles.z;
        Vector3 direction = transform.right;

        // Find sensor location
        Vector3 sensor_position = transform.position + offset * direction;

        // Create array to store sensor readings
        float[] raycast_distances = new float[raycast_angle_degrees.Length];

        for (int i = 0; i < raycast_angle_degrees.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(sensor_position, Quaternion.AngleAxis(raycast_angle_degrees[i], Vector3.back) * direction);
            if (hit.collider != null)
            {
                Debug.DrawLine(sensor_position, hit.point);
                raycast_distances[i] = hit.distance;
            }
        }

    }
}
