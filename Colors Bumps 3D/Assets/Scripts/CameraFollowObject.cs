using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    public Transform target;
    public Vector3 target_Offset;
    private void Start()
    {
        target_Offset = transform.position - target.position;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
          //  if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + target_Offset, 0.01f);
        }
    }
}