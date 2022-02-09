using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    public GameObject ScopeOverlay;
    public GameObject CameraWeapon;
    private bool IsScope = false;
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
                IsScope = !IsScope;
                animator.SetBool("Scoped", IsScope);
                if (IsScope)
                {
                    StartCoroutine(onScoped());
                }
                else
                {
                    OnUnscoped();
                }
        }
    }
    void OnUnscoped()
    {
        ScopeOverlay.SetActive(false);
        CameraWeapon.SetActive(true);
    }
    IEnumerator onScoped()
    {
        yield return new WaitForSeconds(.25f);
        ScopeOverlay.SetActive(true);
        CameraWeapon.SetActive(false);
    }
}
