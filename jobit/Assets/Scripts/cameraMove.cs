using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class cameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public Transform Target;
    public Vector3 targetPos;
    public Vector3 dop;
    public Vector3 velocyty;
    public void Start()
    {
        if(speed == 0)
        {
            speed = 0.1f;
        }
    }
    void LateUpdate()
    {
        if (Target != null)
        {
            targetPos = Target.position;
            targetPos.y = transform.position.y;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos + dop, ref velocyty, speed);
        }
    }
}
