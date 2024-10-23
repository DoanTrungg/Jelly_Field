using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;
    public bool FollowCursor { get; set; } = true;
    public Vector3 StarPosition;
    public bool CanDrag { get; set; } = true;
    Canvas canvas;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CanDrag) return;

        OnBeginDragHandler?.Invoke(eventData);
    }
    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!CanDrag) return;

        OnBeginDragHandler?.Invoke(eventData);

        if (FollowCursor)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (!CanDrag) return;


        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        DropArea dropArea = null;

        foreach (var result in results)
        {
            dropArea = result.gameObject.GetComponent<DropArea>();

            if (dropArea != null)
            {
                break;
            }
        }

        if (dropArea != null)
        {
            if (dropArea.Accept(this))
            {
                dropArea.Drop(this);
                gameObject.GetComponent<CanvasGroup>().interactable = false;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                OnEndDragHandler?.Invoke(eventData, true);
                return;
            }
        }
        rectTransform.anchoredPosition = StarPosition;
        OnEndDragHandler?.Invoke(eventData, false);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        StarPosition = rectTransform.anchoredPosition;
    }
}
