using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject blackScrene;

    public static FadeController Instance { get; set; }

    private void Start()
    {
        Instance = this;
        EnableObject();
    }

    public void ShowFade()
    {
        StartCoroutine(DoShowFade());
    }

    public void HideFade()
    {
        StartCoroutine(DoHideFade());
    }

    public void EnableObject()
    {
        blackScrene.SetActive(true);
        HideFade();
    } 

    IEnumerator DoShowFade()
    {
        blackScrene.SetActive(true);
        anim.SetBool("isHide", false);
        anim.SetBool("isShow", true);
        yield return new WaitForSeconds(1f);
        blackScrene.SetActive(false);
    }

    IEnumerator DoHideFade()
    {
        blackScrene.SetActive(true);
        anim.SetBool("isShow", false);
        anim.SetBool("isHide", true);
        yield return new WaitForSeconds(1f);
        blackScrene.SetActive(false);
    }
}
