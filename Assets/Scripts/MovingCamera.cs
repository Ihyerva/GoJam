using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField] private Transform goal;

    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, goal.position, 2f * Time.deltaTime);
    }
}
