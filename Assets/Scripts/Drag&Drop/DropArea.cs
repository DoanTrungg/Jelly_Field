using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public List<DropCondition> DropConditions;
    public event Action<DraggableComponent> OnDropHandler;
    private void Start()
    {
        DropConditions = new List<DropCondition>();
        OnDropHandler += OnItemDropped;
    }
    public bool Accept(DraggableComponent draggble)
    {
        return DropConditions.TrueForAll(cond => cond.Check(draggble));
    }

    public void Drop(DraggableComponent draggble)
    {
        OnDropHandler?.Invoke(draggble);
    }
    private void OnItemDropped(DraggableComponent draggable)
    {

        draggable.transform.position = transform.position;
    }
}
