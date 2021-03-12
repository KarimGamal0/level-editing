using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    [SerializeField]
    Path path;
    [SerializeField]
    float speed = 10;
    Rigidbody rigidbody;
    Animator animator;
    Vector3 direction;
    Vector3 destination;
    Vector3 force;
    [SerializeField]
    float range = 5;
    [SerializeField]
    float rotationFact;
    int? currentPointIndex;
    Vector3 currentPointPosition;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        if (currentPointIndex == null)
        {
            (currentPointIndex, currentPointPosition) = path.GetNearestChild(transform.position);

        }
        else if (Vector3.Distance(transform.position, currentPointPosition) < 1)
        {
            (currentPointIndex, currentPointPosition) = path.GetNextChild(currentPointIndex.Value);
        }
        direction = (currentPointPosition - transform.position);
        destination = direction.normalized;
        transform.forward = direction;

        force = destination * speed;
        if (force.magnitude > 0)
            Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination), Time.deltaTime * rotationFact);
        // Debug.Log(force);
        //animator.SetFloat("Speed", speed);

        rigidbody.velocity = force;
        //Debug.Log();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(currentPointPosition, 0.5f);
        Gizmos.DrawRay(transform.position, direction);
    }
}
