using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMove : MonoBehaviour
{
    public Vector3 vel = Vector3.zero;
    public Vector3 acc = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float speed;
    public float mass = 1;
    public float maxSpeed;
    public float maxForce;

    public Vector3 target;
    public Transform targetTransform;

    public float slowDist = 1;
    public float damping = 1;

    public Spawner spawner;
    public TrafficLights targetLight;

    // Start is called before the first frame update
    void Start()
    {
        mass = 1;
        maxForce = 20;
        maxSpeed = 10;
        spawner = (Spawner)FindObjectOfType(typeof(Spawner));
        targetTransform = spawner.lights[Random.Range(0, spawner.lights.Length)].transform;
        targetLight = targetTransform.GetComponent<TrafficLights>();
        var renderer = this.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.blue);
        CalculateForce();
    }

    public Vector3 CalculateForce()
    {
        Vector3 force = Vector3.zero;
        force += MoveToLight(target);
        return force;
    }

    Vector3 MoveToLight(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dis = toTarget.magnitude;

        float ramped = (dis / slowDist) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = (toTarget / dis) * clamped;

        return desired - vel;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetLight.isGreen == false)
        {
            targetTransform = spawner.lights[Random.Range(0, spawner.lights.Length)].transform;
            targetLight = targetTransform.GetComponent<TrafficLights>();
        }
        if (Vector3.Distance(this.transform.position, targetTransform.transform.position)< 0.5)
        {
            targetTransform = spawner.lights[Random.Range(0, spawner.lights.Length)].transform;
            targetLight = targetTransform.GetComponent<TrafficLights>();
        }
        if (targetTransform != null)
        {
            target = targetTransform.position;
        }
        force = CalculateForce();
        acc = force / mass;
        vel += acc * Time.deltaTime;

        transform.position += vel * Time.deltaTime;
        speed = vel.magnitude;

        if (speed > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + acc, Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + vel, tempUp);
            vel -= (damping * vel * Time.deltaTime);
        }
    }

}

