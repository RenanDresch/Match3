using UnityEngine;

namespace Game.Logic
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Gem : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private SpriteRenderer spriteRenderer = default;

        #endregion

        #region Public Properties

        public SpriteRenderer SpriteRenderer => spriteRenderer;

        public int Row;
        public int Column;

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