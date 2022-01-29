using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Blank;

public class MyButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        GoOnHover();
    }

    public void OnPointerExit(PointerEventData eventData) {
        GoBackToNormal();
    }

    public void GoOnHover() {
        animator.SetBool("nowHover", true);
        animator.SetBool("goBack", false);
    }

    public void GoBackToNormal() {
        animator.SetBool("goBack", true);
        animator.SetBool("nowHover", false);
    }

    public void OnPointerClick(PointerEventData eventData) {
        SFX.instance.PlayClick();
    }
}
