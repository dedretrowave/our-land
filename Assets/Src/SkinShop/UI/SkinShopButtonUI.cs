using UnityEngine;
using UnityEngine.UI;

namespace Src.SkinShop.UI
{
    public class SkinShopButtonUI : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _purchaseButton;

        public void ChangeButtonByPrice(int price = 0)
        {
            if (price > 0)
            {
                _purchaseButton.gameObject.SetActive(true);
                _selectButton.gameObject.SetActive(false);
            }
            else
            {
                _purchaseButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
            }
        }
    }
}