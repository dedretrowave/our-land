using Characters.Skins;

namespace SkinShop.Models
{
    public class SkinShopModel
    {
        private Skin _skin;

        public Skin Skin => _skin;

        public void SetSkinItem(SkinItem item)
        {
            _skin.SetItem(item);
        }

        public void SetSkin(Skin skin)
        {
            _skin = skin;
        }
    }
}