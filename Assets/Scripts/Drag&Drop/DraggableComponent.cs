using DG.Tweening;
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
    private Vector3 oldPosition;
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
        oldPosition = transform.localPosition;
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
        bool hide = false;
        if (dropArea != null)
        {
            hide = dropArea.gameObject.GetComponent<BackgroundTile>().hide;
            if (dropArea.Accept(this) && hide)
            {
                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(eventData, true);
                Unihide(dropArea);
                AnimToTarget();
                return;
            }
        }
        for (int i = 0; i < gameObject.GetComponent<BackgroundTile>().ListDot.Count; i++)
        {
            Debug.Log(gameObject.GetComponent<BackgroundTile>().ListDot[i].name + "name");
            Debug.Log(gameObject.GetComponent<BackgroundTile>().ListDot[i].Id + "name_id");
        }
        rectTransform.anchoredPosition = StarPosition;
        OnEndDragHandler?.Invoke(eventData, false);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        StarPosition = rectTransform.anchoredPosition;
    }
    private void AnimToTarget()
    {
        gameObject.GetComponent<Dofade>().FadeOut(0).OnComplete(() =>
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        });
        DOVirtual.DelayedCall(0.5f, () =>
        {
            gameObject.GetComponent<Dofade>().FadeIn(0.2f);
        });
    }
    private void Unihide(DropArea dropArea)
    {
        gameObject.GetComponent<BackgroundTile>().CopyColor(gameObject.GetComponent<BackgroundTile>(),dropArea.GetComponent<BackgroundTile>());
    }
}
