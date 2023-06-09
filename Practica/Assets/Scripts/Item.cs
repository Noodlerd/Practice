using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool isFreeze=true;
    public bool isOutput=false;

    private bool conditionMet = false;
    private float conditionTimer = 0f;

    public static Item Instance { get; set; }

    private void Start()
    {
        Instance = this;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void Update()
    {
        if (Spawner.Instance.isEnableToOutput && Input.GetKey(KeyCode.Mouse0) && Spawner.Instance.isSpawned&&!isOutput)
        {
            ClawController.Instance.OpenClaws();
            rb.bodyType = RigidbodyType2D.Dynamic;
            isOutput = true;
            transform.SetParent(null);
            Score.Instance.PlusCurrentScore();
            isFreeze = false;
        }

        if (CheckCondition())
        {
            if (!conditionMet)
            {
                // ≈сли проверка успешна в первый раз, запускаем таймер
                conditionTimer = Time.time;
                conditionMet = true;
            }
            else if (Time.time - conditionTimer >= 0.7f)
            {
                // ≈сли прошло 2 секунд с момента первого успешного прохождени€ проверки, выполн€ем действие
                StartCoroutine(Freeze());
            }
        }
        else
        {
            // ≈сли проверка не проходит, сбрасываем состо€ние
            conditionMet = false;
        }

    }

    private bool CheckCondition()
    {
        //проверка услови€
        return rb.bodyType == RigidbodyType2D.Dynamic && rb.velocity == Vector2.zero && rb.angularDrag == 0 && !isFreeze;
    }

    private IEnumerator Freeze()
    {
        isFreeze = true;
        Debug.Log("я фриз");
        rb.bodyType = RigidbodyType2D.Static;
        CameraMover.Instance.CheckHighestItem(transform.position);
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }

}
