using UnityEngine;

namespace Game.Models
{
    public static class Constants
    {
        public const int Cache8 = 1 << 3;
        public const int Cache16 = 1 << 4;
        public const int Cache32 = 1 << 5;
        public const int Cache64 = 1 << 6;
        public const int Cache128 = 1 << 7;
        public const int Cache256 = 1 << 8;
        public const int Cache512 = 1 << 9;
        public const int Cache1024 = 1 << 10;
        public const int Cache2048 = 1 << 11;
        public const int Cache4096 = 1 << 12;
        public const int Cache8192 = 1 << 13;

        #region Navigation

        public const int InitialViewCapacity = Cache64;

        #endregion

        public const int InitialEventsCapacity = Cache32;
        public const int InitialPhysicsBufferCapacity = Cache32;

        public const int InitialMaterialCapacity = Cache64;

        #region Data

        public const int UserCharactersCapacity = Cache16;
        public const int MetagameCapacity = Cache128;
        public const int PlayersCapacity = Cache256;
        public const int InventoriesCapacity = Cache16;
        
        #endregion

        #region Visual

        public const int InitialTrailPool = Cache8;

        #endregion

        #region Addressables

        public const int ScenesCapacity = Cache8;
        public const int LevelLoadRequestCapacity = Cache128;
        public const int LevelCapacity = Cache4096;
        public const int DisposedLevelBlocksCapacity = Cache128;
        public const int SpawnPointCapacity = Cache16;
        public const int AddressablesCapacity = Cache64;
        public const int AddressablesOperationsCapacity = Cache32;

        public const int XBlockAmount = 30;
        public const int YBlockAmount = 60;
        public const int ZBlockAmount = 30;

        #endregion

        #region Audio

        public const int InitialAudioFXPool = Cache32;
        public const int InitialAudioClipCapacity = Cache64;

        #endregion

        #region Entities

        public const int InitialLocallyControlledEntities = Cache8;
        public const int InitialEntityCapacity = Cache2048;
        public const int InitialUpdatablesCapacity = Cache16;
        public const int InitialFixedUpdatablesCapacity = Cache8;
        public const int InitialDisposedIdsCapacity = Cache8;
        public const int AttackCapacity = Cache512;

        #endregion

        #region Bullets

        public const int InitialBulletEmitterCapacity = Cache32;
        public const int BulletBufferCapacity = Cache4096;
        public const int BulletRecordedHitCapacity = Cache16;

        #endregion

        #region Shading

        public const string CharacterMaterialShaderName = "Shader Graphs/LevelShader";

        const string CharacterMaterialMainColorPropertyName = "_RedChannelColor";
        const string CharacterMaterialAccentColorPropertyName = "_GreenChannelColor";
        const string CharacterMaterialGlowColorPropertyName = "_EmissiveColor";
        const string CharacterMaterialMainTexturePropertyName = "_RedChannelTexture";
        const string CharacterMaterialAccentTexturePropertyName = "_GreenChannelTexture";
        const string CharacterMaterialTilingPropertyName = "_TriplanarTilingAndBlend";
        const string CharacterMaterialLocalSpaceTilingPropertyName = "_UseSelfPosition";

        public static readonly int CharacterMaterialMainColorPropertyId =
                Shader.PropertyToID(CharacterMaterialMainColorPropertyName);

        public static readonly int CharacterMaterialAccentColorPropertyId =
                Shader.PropertyToID(CharacterMaterialAccentColorPropertyName);

        public static readonly int CharacterMaterialGlowColorPropertyId =
                Shader.PropertyToID(CharacterMaterialGlowColorPropertyName);

        public static readonly int CharacterMaterialMainTexturePropertyId =
                Shader.PropertyToID(CharacterMaterialMainTexturePropertyName);

        public static readonly int CharacterMaterialAccentTexturePropertyId =
                Shader.PropertyToID(CharacterMaterialAccentTexturePropertyName);

        public static readonly int CharacterMaterialTilingPropertyId =
                Shader.PropertyToID(CharacterMaterialTilingPropertyName);

        public static readonly int CharacterMaterialLocalSpaceTilingPropertyId =
                Shader.PropertyToID(CharacterMaterialLocalSpaceTilingPropertyName);

        #endregion
    }
}