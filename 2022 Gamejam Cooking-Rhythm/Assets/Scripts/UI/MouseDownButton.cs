using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class MouseDownButton : Selectable, IPointerDownHandler, ISubmitHandler
{
    public UnityEvent mouseDownEvent;
    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDownEvent?.Invoke();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        mouseDownEvent?.Invoke();
    }
}
