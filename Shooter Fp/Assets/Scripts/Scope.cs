using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    public GameObject ScopeOverlay;
    private bool IsScope = false;
    private void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            IsScope = !IsScope;
            animator.SetBool("Scoped", IsScope);
            ScopeOverlay.SetActive(IsScope);
        
        

        }
    }
}
