using Game.Contracts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Logic
{
    [RequireComponent(typeof(RectTransform))]
    public class PointerListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        #region Constants

        private const float MIN_DRAG_OFFSET = 20;

        #endregion

        #region Fields

        [SerializeField]
        private RectTransform rectTransform = default;
        [SerializeField]
        private Camera cameraComponent = default;
        [SerializeField]
        private SelectionController selectionController = default;

        private bool dragging = false;
        private Vector2 pointerDownPosition;

        public void OnDrag(PointerEventData eventData)
        {
            if(Vector2.Distance(eventData.position, pointerDownPosition) > MIN_DRAG_OFFSET)
            {
                dragging = true;
            }
        }

        #endregion

        #region Public Methods

        public void OnPointerDown(PointerEventData eventData)
        {
            dragging = false;
            pointerDownPosition = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var relativePositionX = (pointerDownPosition - (Vector2)rectTransform.position).x;
            relativePositionX /= rectTransform.rect.width * rectTransform.lossyScale.x;
            relativePositionX *= cameraComponent.orthographicSize * 2;

            var relativePositionY = (pointerDownPosition - (Vector2)rectTransform.position).y;
            relativePositionY /= rectTransform.rect.height * rectTransform.lossyScale.y;
            relativePositionY *= cameraComponent.orthographicSize * 2;

            var position = new Vector2(relativePositionX, relativePositionY);

            if (!dragging)
            {
                var hit = Physics2D.Raycast(position, Vector2.zero);
                if (hit)
                {
                    var gem = hit.transform.GetComponent<Gem>();
                    if (gem)
                    {
                        selectionController.SelectGem(gem);
                    }
                }
            }
            else
            {
                var swipeVector = eventData.position - pointerDownPosition;

                var swipeDirection = SwipeDirection.None;
                if(Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
                {
                    swipeDirection = swipeVector.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                }
                else
                {
                    swipeDirection = swipeVector.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                }

                var hit = Physics2D.Raycast(position, Vector2.zero);
                if (hit)
                {
                    var gem = hit.transform.GetComponent<Gem>();
                    if (gem)
                    {
                        selectionController.SelectGem(gem, swipeDirection);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private void Reset()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        #endregion
    }
}