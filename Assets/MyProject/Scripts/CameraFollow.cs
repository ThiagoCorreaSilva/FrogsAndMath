using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset.z = transform.position.z;
    }

    private void Update()
    {
        Vector3 _newPos = player.position + offset;
        Vector3 _smoothPos = Vector3.Lerp(transform.position, _newPos, smoothSpeed);
        transform.position = _smoothPos;
    }
}
