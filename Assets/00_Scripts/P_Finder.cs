using System;
using System.Collections.Generic;
using UnityEngine;

public class P_Finder : MonoBehaviour
{
    [SerializeField] private float checkRadius = 5.0f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private GameObject IconPrefab;

    [SerializeField] private float activationDistance = 3.0f;
    
    private Dictionary<Transform, GameObject> activeIcons = new Dictionary<Transform, GameObject>();

    private void Update()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, checkRadius, interactableLayer);

        Transform closetObject = null;
        float closetDistance = Mathf.Infinity;
        
        HashSet<Transform> currentObjects = new HashSet<Transform>();
        
        foreach (Collider obj in nearbyObjects)
        {
            Transform targetTransform = obj.transform;
            
            float distance = Vector3.Distance(obj.transform.position, transform.position);

            if (distance <= activationDistance && distance < closetDistance)
            {
                closetObject = targetTransform;
                closetDistance = distance;
                

            }
        }

        if (closetObject != null)
        {
            ShowIcon(closetObject);

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F");
            }
        }

        List<Transform> toRemove = new List<Transform>();
        foreach (var iconEntry in activeIcons)
        {
          if (iconEntry.Key != closetObject)
          {
              iconEntry.Value.GetComponent<UI_Animation_Handler>().AnimationChange(("Out"));
              toRemove.Add(iconEntry.Key);
          }
        }

        foreach (var transformToRemove in toRemove)
        {
             activeIcons.Remove(transformToRemove);
        }
    }

    private void ShowIcon(Transform targetTransform)
    {
        if (activeIcons.ContainsKey(targetTransform))
        {
            UpdateIconPosition(targetTransform, activeIcons[targetTransform]);
            return;
        }

        GameObject iconInstance = Instantiate(IconPrefab, uiCanvas.transform);
        activeIcons[targetTransform] = iconInstance;
    }

    void UpdateIconPosition(Transform targetTransform, GameObject icon)
    {
        Vector3 ScreenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        
        icon.GetComponent<RectTransform>().position = ScreenPosition;
    }
}
