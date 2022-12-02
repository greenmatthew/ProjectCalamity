using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PC.UI
{
    public class ScrollRectNoMouseDrag : ScrollRect
    {
        // Disables the mouse drag functionality of the ScrollRect.
        public override void OnBeginDrag(PointerEventData eventData) {}
        public override void OnDrag(PointerEventData eventData) {}
        public override void OnEndDrag(PointerEventData eventData) {}
    }
}