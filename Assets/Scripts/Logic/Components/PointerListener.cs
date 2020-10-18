using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Logic
{
    [RequireComponent(typeof(RectTransform))]
    public class PointerListener : MonoBehaviour, IPointerDownHandler
    {
        #region Fields

        [SerializeField]
        private RectTransform rectTransform = default;
        [SerializeField]
        private Camera cameraComponent = default;
        [SerializeField]
        private SelectionController selectionController = default;

        #endregion

        #region Public Methods

        public void OnPointerDown(PointerEventData eventData)
        {
            var relativePositionX = (eventData.position - (Vector2)rectTransform.position).x;
            relativePositionX /= rectTransform.rect.width * rectTransform.lossyScale.x;
            relativePositionX *= cameraComponent.orthographicSize * 2;

            var relativePositionY = (eventData.position - (Vector2)rectTransform.position).y;
            relativePositionY /= rectTransform.rect.height * rectTransform.lossyScale.y;
            relativePositionY *= cameraComponent.orthographicSize * 2;

            var position = new Vector2(relativePositionX, relativePositionY);

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

        #endregion

        #region Private Methods

        private void Reset()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        #endregion
    }
}