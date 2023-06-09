using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseZone : MonoBehaviour
{
    [SerializeField] private GameObject LoseCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            StartCoroutine(DoActiveLoseCanvas());
        }
    }

    IEnumerator DoActiveLoseCanvas()
    {
        LoseCanvas.SetActive(true);
        BG.Instance.isStopingAnim = true;
        ClawController.Instance.isStopingMoving = true;
        Spawner.Instance.isEnableToOutput = false;
        yield return new WaitForSeconds(0.1f);
    }


}
