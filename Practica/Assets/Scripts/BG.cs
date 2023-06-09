using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f; 

    private float backgroundWidth; 
    private Vector2 startPosition; 

    public bool isStopingAnim=false;

    public static BG Instance { get; set; }

    private void Start()
    {
        Instance = this;
        backgroundWidth = GetComponentInChildren<SpriteRenderer>().bounds.size.x; 
        startPosition = transform.position; 
    }

    private void Update()
    {
        if (isStopingAnim&&scrollSpeed > 0.01f)
            StopAnim();

        float offset = Time.time * scrollSpeed;
        float newPosition = Mathf.Repeat(offset, backgroundWidth);
        transform.position =new Vector2(startPosition.x+ newPosition,transform.position.y);
    }

    public void StopAnim()
    {
        scrollSpeed = Mathf.Lerp(scrollSpeed, 0, Time.deltaTime / 1.421f);//Время анимации 1.421f
    }
}
