using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (Player.Instance != null)
        {
            rect.position = Camera.main.WorldToScreenPoint(Player.Instance.transform.position);
        }
        else
        {
            Debug.LogWarning("Player.Instance is null. Check if the Player object is in the scene.");
        }
    }
}
