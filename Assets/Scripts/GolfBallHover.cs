using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GolfBallHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image image;
    private Color originalColor;
    private bool isSelected = false;

    private static GolfBallHover currentlySelected = null;

    public float slotID;

    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            originalColor = image.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null && !isSelected)
        {
            Color hoverColor = image.color;
            hoverColor.b = 190f / 255f;
            image.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null && !isSelected)
        {
            image.color = originalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (image == null) return;

        if (currentlySelected != null && currentlySelected != this)
        {
            currentlySelected.Deselect();
        }

        if (currentlySelected == this)
        {
            Deselect();
        }
        else
        {
            Select();
        }
    }

    private void Select()
    {
        float avg = (image.color.r + image.color.g + image.color.b) / 3f;
        image.color = new Color(avg, avg, avg, image.color.a);
        isSelected = true;
        currentlySelected = this;

        Debug.Log($"Selected slot ID: {slotID}");
    }

    private void Deselect()
    {
        image.color = originalColor;
        isSelected = false;

        if (currentlySelected == this)
            currentlySelected = null;
    }
}
