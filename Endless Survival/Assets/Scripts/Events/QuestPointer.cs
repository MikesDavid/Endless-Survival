using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed;


    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.position-transform.position),
            rotationSpeed * Time.deltaTime);
    }
}