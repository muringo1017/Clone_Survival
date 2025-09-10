using System;
using UnityEngine;

public class CamerasMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float posX;
    [SerializeField] private float posY;   
    [SerializeField] private float posZ;

    [SerializeField] private float moveSpeed = 2.0f;

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(
            player.transform.position.x + posX,
            player.transform.position.y + posY,
            player.transform.position.z + posZ
            ), Time.deltaTime * moveSpeed);
    }
    
}
