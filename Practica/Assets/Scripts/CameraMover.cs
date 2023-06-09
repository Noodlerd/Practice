using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Vector3 highestItemPosition;
    private Vector3 targetPosition;


    [SerializeField] private float speed = 1.0f;
    public bool isMoving = false;

    private float endY;
    private float startY;
    private float expandToY = 0f;

    public static CameraMover Instance { get; set; }

    void Start()
    {
        Instance = this;
        highestItemPosition = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (isMoving)
        {
            float step = speed * Time.deltaTime; 

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step); 

            if (transform.position == targetPosition) // ѕроверка на достижение конечной позиции камеры
            {
                isMoving = false;
            }
        }
    }

    public void CheckHighestItem(Vector3 last)
    {
        if (last.y > highestItemPosition.y&&!isMoving)
        {
            expandToY = last.y - highestItemPosition.y;
            highestItemPosition = last;
            startY = transform.position.y;
            endY = startY + expandToY;
            targetPosition = new Vector3(targetPosition.x, endY, targetPosition.z);
            isMoving = true;
        }

    }
}
