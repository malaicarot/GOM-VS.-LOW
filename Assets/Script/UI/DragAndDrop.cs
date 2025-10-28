using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Canvas canvas;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;

    public event Action OnDropChecking;

    void Awake()
    {
        canvas = UIManagers.Instance.GetCanvans();
        rectTransform = this.GetComponent<RectTransform>();
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("On Pointer Down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag!");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        rectTransform.transform.SetParent(canvas.GetComponent<RectTransform>().transform, true);

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag!");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;


        // if(eventData.pointerDrag == null || eventData.pointerDrag?.GetComponent<SlotItemDrop>() == null)
        // {
        //     Debug.Log("Don't exits!");
        // }
        // else
        // {
        //     Debug.Log("Exits!");
        // }

        // if (eventData.pointerEnter == null || eventData.pointerEnter?.GetComponent<SlotItemDrop>() == null)
        // {
        //     rectTransform.SetParent(UIManagers.Instance.ReturnBrewContent().transform, false);
        // }
    }


    public void ResetParent(Transform _rectTransform)
    {
        this.rectTransform.SetParent(_rectTransform, false);
    }
}
