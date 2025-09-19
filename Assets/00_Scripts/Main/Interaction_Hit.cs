using System;
using System.Collections;
using UnityEngine;

public class Interaction_Hit : M_Object
{
    private float shakeAmount = 5.0f;
    private float ShakeDuration = 0.5f;

    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    public override void Interaction()
    {
        PlayerMovement.instance.AnimatorChange(m_Data.m_Type.ToString());
        base.Interaction();
    }

    public override void HP_Init()
    {
        base.HP_Init();
        ShakeTree(transform.position - PlayerMovement.instance.transform.position);
    }

    private void ShakeTree(Vector3 attackDirection)
    {
        Vector3 oppositeDirection = attackDirection.normalized;

        Quaternion targetRoation = Quaternion.Euler(
            originalRotation.eulerAngles.x + oppositeDirection.z * shakeAmount,
            originalRotation.eulerAngles.y,
            originalRotation.eulerAngles.z + oppositeDirection.x * shakeAmount);

        StopAllCoroutines();
        StartCoroutine(ShakeAnimation(targetRoation));
    }

    private IEnumerator ShakeAnimation(Quaternion targetRotation)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < ShakeDuration / 2.0f)
        {
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, 
                elapsedTime/ (ShakeDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        elapsedTime = 0.0f;

        while (elapsedTime < ShakeDuration / 2.0f)
        {
            transform.rotation = Quaternion.Slerp(targetRotation,originalRotation, 
                elapsedTime/ (ShakeDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
