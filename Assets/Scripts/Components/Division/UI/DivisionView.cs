using Characters.Skins;
using UnityEngine;
using UnityEngine.UI;

namespace Components.Division.UI
{
    public class DivisionView : MonoBehaviour
    {
        [SerializeField] private Image _flag;
        [SerializeField] private Image _eyes;

        public void SetSkin(Skin skin)
        {
            _flag.sprite = skin.GetItemByType(SkinItemType.Flag).Sprite;
            _eyes.sprite = skin.GetItemByType(SkinItemType.Eyes).Sprite;
        }
    }
}