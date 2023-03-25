using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Src.MainMenu
{
    public class TransitionToLevel : MonoBehaviour
    {
        [SerializeField] private SceneAsset _levelScene;

        public void StartGame()
        {
            SceneManager.LoadScene(_levelScene.name);
        }
    }
}