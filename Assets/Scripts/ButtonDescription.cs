using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public UIManager uiManager;
    public string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiManager.SetDescription(description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiManager.SetDescription("");
    }

    // IMPORTANT! so button doesn't stay in highlighted color when being clicked and released outside of the button area.
    public void OnPointerUp(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

}
