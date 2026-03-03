using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DynamicObject dO;
    private ToolTipManager ttm;

    private void Awake()
    {
        ttm = FindAnyObjectByType<ToolTipManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ttm.show(dO, gameObject.transform, dO.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ttm.hide();
    }
}