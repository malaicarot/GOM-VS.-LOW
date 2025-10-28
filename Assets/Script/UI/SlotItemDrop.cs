using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItemDrop : MonoBehaviour, IDropHandler
{
    RectTransform parentRectTransform;
    public event Action OnUltimateHealing;
    public event Action OnRestoreMana;
    public event Action OnCriticalTime;
    public event Action OnSetCooldown;


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("On Drop!");
        var item = eventData.pointerDrag?.GetComponent<RectTransform>();
        Image image = item?.GetComponentInChildren<Image>(true);
        if (item != null)
        {
            parentRectTransform = parentRectTransform != null ? parentRectTransform : UIManagers.Instance.ReturnSlotParentComponent();
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(parentRectTransform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            PlayerInventory.Instance.GetItem(image.sprite);
        }
    }
}
