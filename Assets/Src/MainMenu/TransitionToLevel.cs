using UnityEngine;
using UnityEngine.SceneManagement;

namespace Src.MainMenu
{
    public class TransitionToLevel : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Level");
        }
    }
}