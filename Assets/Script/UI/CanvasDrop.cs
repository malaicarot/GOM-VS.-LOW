using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var item = eventData?.pointerDrag?.GetComponent<DragAndDrop>();
        if (item != null)
        {
            item.ResetParent(UIManagers.Instance.ReturnBrewContent().transform);  
        }
    }
}
