using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public List<DropCondition> DropConditions = new List<DropCondition>();
    public event Action<DraggableComponent> OnDropHandler;

    public bool Accept(DraggableComponent draggble)
    {
        return DropConditions.TrueForAll(cond => cond.Check(draggble));
    }

    public void Drop(DraggableComponent draggble)
    {
        OnDropHandler?.Invoke(draggble);
    }
}
