using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject lightsPrefab;
    public GameObject carPrefab;
    public GameObject[] lights;
    float radius = 10;




    // Start is called before the first frame update
    void Start()
    {
        lights = new GameObject[10];
        for (int i = 0; i < lights.Length; i++)
        {
            float angle = i * Mathf.PI * 2 / lights.Length;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject clone = Instantiate(lightsPrefab, pos, rot);

            lights[i] = clone;
        }

        Instantiate(carPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //targetLight = car.targetTransform.GetComponent<TrafficLights>();
        //if (targetLight.isGreen == false)
       // {
        //    car.targetTransform = lights[Random.Range(0, lights.Length)].transform;
        //}

    }
}
