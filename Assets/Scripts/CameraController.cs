using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 10f;

    private void LateUpdate()
    {
        Vector3 camPos = transform.position;
        camPos.x = Mathf.Max(camPos.x, Mathf.MoveTowards(camPos.x, player.position.x, followSpeed * Time.deltaTime));
        transform.position = camPos;
    }
}
