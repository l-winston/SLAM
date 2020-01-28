using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public Vector3[] points;

    public Data(Vector3[] points)
    {
        this.points = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            this.points[i] = new Vector3(points[i].x, points[i].y, points[i].z);
        }
    }

    public static Data ScanPoints(Vector3 sensor_position, Vector3 direction, float[] raycast_angle_degrees)
    {
        int number_readings = raycast_angle_degrees.Length;
        // Create array to store sensor readings
        float[] raycast_distances = new float[number_readings];
        Vector3[] intersection_points = new Vector3[number_readings];

        for (int i = 0; i < number_readings; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(sensor_position, Quaternion.AngleAxis(raycast_angle_degrees[i], Vector3.back) * direction);
            if (hit.collider != null)
            {
                Debug.DrawLine(sensor_position, hit.point);
                raycast_distances[i] = hit.distance;
                intersection_points[i] = hit.point;
                // Debug.Log("Human = " + string.Join(" ", new List<float>(raycast_distances).ConvertAll(x => Math.Round(x, 2).ToString())));
            }
        }

        return new Data(intersection_points);
    }
}
