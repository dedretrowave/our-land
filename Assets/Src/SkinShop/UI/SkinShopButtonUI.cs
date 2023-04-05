using UnityEngine;
using UnityEngine.UI;

namespace Src.SkinShop.UI
{
    public class SkinShopButtonUI : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Button _zeroPriceButton;
        [SerializeField] private Button _notZeroPriceButton;

        public void ChangeButtonByPrice(int price = 0)
        {
            if (price > 0)
            {
                _notZeroPriceButton.gameObject.SetActive(true);
                _zeroPriceButton.gameObject.SetActive(false);
            }
            else
            {
                _notZeroPriceButton.gameObject.SetActive(false);
                _zeroPriceButton.gameObject.SetActive(true);
            }
        }
    }
}