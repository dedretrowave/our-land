using Src.SkinShop.Items;
using Src.SkinShop.Items.Base;
using UnityEngine;

namespace Src.Map.Fraction
{
    [CreateAssetMenu(menuName = "Game Entities/Fraction", fileName = "Fraction", order = 0)]
    public class Fraction : ScriptableObject
    {
        [SerializeField] private int _id;
        
        [Header("Components")]
        [SerializeField] private Skin _skin;
        [SerializeField] private Color _color;

        [Header("Parameters")]
        [SerializeField] private bool _allowsDivisionGeneration;
        [SerializeField] private bool _isPlayerControlled;

        public int Id => _id;
        public Skin Skin => _skin;
        public Sprite Flag => _skin.GetItemByType(SkinItemType.Flag).Sprite;
        public Sprite Eyes => _skin.GetItemByType(SkinItemType.Eyes).Sprite;
        public Color Color => _color;
        public bool AllowsDivisionGeneration => _allowsDivisionGeneration;
        public bool IsPlayerControlled => _isPlayerControlled;

        public void SetSkin(Skin skin)
        {
            if (skin.Items.Count != _skin.Items.Count)
            {
                return;
            }
            
            _skin = new Skin(skin);
        }
    }
}