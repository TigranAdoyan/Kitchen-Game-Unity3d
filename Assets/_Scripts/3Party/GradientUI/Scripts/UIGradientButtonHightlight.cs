using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient Button Highlight")]
public class UIGradientButtonHightlight : UIGradient, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    private Button button;

    protected override void Start()
    {
        base.Start();
        button = GetComponent<Button>();
        UpdateHighlightedColor();
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        base.ModifyMesh(vh);
        UpdateHighlightedColor();
    }
    public void UpdateHighlightedColor()
    {
        if (button != null)
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.highlightedColor = Color.Lerp(m_color2, m_color1, 0.5f);
            button.colors = colorBlock;
        }
        if (button != null)
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.highlightedColor = Color.Lerp(m_color2, m_color1, 0.5f);
            colorBlock.pressedColor = Color.Lerp(m_color2, m_color1, 0.7f); 
            button.colors = colorBlock;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.OnPointerEnter(eventData);
        UpdateHighlightedColor();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        button.OnPointerExit(eventData);
        UpdateHighlightedColor();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        button.OnPointerUp(eventData);
        button.OnPointerExit(eventData);
        UpdateHighlightedColor();

        BaseEventData baseEventData = new BaseEventData(EventSystem.current);
        button.OnDeselect(baseEventData);
        if (button.IsInteractable() && button.IsActive())
        {
            button.OnSelect(baseEventData);
        }
    }
}
