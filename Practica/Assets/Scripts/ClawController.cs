using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    [SerializeField] private float distance; // Дистанция движения объекта
    [SerializeField] private float speed ; // Скорость движения объекта
    [SerializeField] private Animator anim;

    private Vector3 startPosition;
    private float leftBound;
    private float rightBound;
    private bool movingRight = true;
    public bool isEnableToMove;
    public bool isStopingMoving = false;

    public bool isAllowToOutput = false;

    public static ClawController Instance { get; set; }

    private void Start()
    {
        Instance = this;
        StartCoroutine(StopClawAtTime());
        startPosition = transform.position;
        leftBound = startPosition.x - distance / 2;
        rightBound = startPosition.x + distance / 2;
    }

    private void FixedUpdate()
    {
        if (isStopingMoving && speed > 0.01f)
            StopAnim();

        if (isEnableToMove)
            MoveClaw();
    }


    private void MoveClaw()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(rightBound, transform.position.y, transform.position.z), speed * Time.fixedDeltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(leftBound, transform.position.y, transform.position.z), speed * Time.fixedDeltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
            }
        }
    }

    IEnumerator StopClawAtTime()
    {
        isEnableToMove = false;
        yield return new WaitForSeconds(1.1f);
        isEnableToMove = true;
    }

    public void StopAnim()
    {
        speed = Mathf.Lerp(speed, 0, Time.deltaTime / 1.421f);//Время анимации 1.421f
    }

    public void OpenClaws()
    {
        StartCoroutine(DoOpenClaws());
    }

    public void CloseClaws()
    {
        StartCoroutine(DoCloseClaws());
    }

    IEnumerator DoOpenClaws()
    {
        anim.SetBool("isOpen", true);
        anim.SetBool("isClose", false);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("isOpen", false);
        anim.SetBool("isClose", true);
    }

    IEnumerator DoCloseClaws()
    {
        anim.SetBool("isOpen", false);
        anim.SetBool("isClose", true);
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("isOpen", true);
        anim.SetBool("isClose", false);
    }
}
