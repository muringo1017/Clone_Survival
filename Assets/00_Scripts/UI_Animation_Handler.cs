using System;
using UnityEngine;

public class UI_Animation_Handler : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimationChange(string temp)
    {
        animator.SetTrigger(temp);
    }
    
    public void DestroyObject() => Destroy(gameObject);
    public void Deactive() => gameObject.SetActive(false);
}
