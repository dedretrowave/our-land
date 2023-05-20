using DI;
using Src.Map.Fraction;
using Src.Saves;
using Src.SkinShop.Skin;
using UnityEngine;

namespace Src.Levels.Level.UI
{
    public class LevelMainMenu : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private LevelUI _ui;
        [SerializeField] private FractionContainer _fractionContainer;
        [SerializeField] private Character _owner;
        
        private PlayerDataSaveSystem _save;

        private void Start()
        {
            _save = DependencyContext.Dependencies.Get<PlayerDataSaveSystem>();

            LevelData data = _save.GetLevelById(_id);

            if (data != null)
            {
                _owner = _fractionContainer.GetFractionById(data.OwnerId);
            }
            
            // _ui.UpdateColorByFraction(_owner);
        }
    }
}