using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMove : MonoBehaviour
{
    public Vector3 vel;
    public Vector3 acc;
    public Vector3 force;

    public float speed;
    public float mass;
    public float maxSpeed;
    public float maxForce;

    public Vector3 target;
    public Transform targetTransform;

    public float slowDist;
    public float damping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null)
        {
            target = targetTransform.position;
        }
        force += MoveToLight(target);
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

    Vector3 MoveToLight(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        float dis = toTarget.magnitude;

        float ramped = (dis / slowDist) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = (toTarget / dis) * clamped;

        return desired - vel;
    }
}
