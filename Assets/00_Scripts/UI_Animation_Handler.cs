using System;
using UnityEngine;

public class UI_Animation_Handler : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimationChange(string temp)
    {
        animator.SetTrigger(temp);
    }
    
    public void DestoryObject() => Destroy(gameObject);
    public void Deactive() => gameObject.SetActive(false);
}
