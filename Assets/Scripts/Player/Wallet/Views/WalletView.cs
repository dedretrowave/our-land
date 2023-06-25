using TMPro;
using UnityEngine;

namespace Player.Wallet.Views
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        public void SetMoney(int amount)
        {
            _moneyText.text = amount.ToString();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}