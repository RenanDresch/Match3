using UnityEngine;

namespace Game.Logic
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Gem : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private SpriteRenderer spriteRenderer = default;

        private GridPosition position = new GridPosition();

        #endregion

        #region Public Properties

        public SpriteRenderer SpriteRenderer => spriteRenderer;

        public GridPosition Position => position;

        public bool Removed { get; set; }

        public bool Equals(Sprite otherSprite)
        {
            return otherSprite == spriteRenderer.sprite;
        }

        #endregion

        #region Private Methods

        private void Reset()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        #endregion
    }
}