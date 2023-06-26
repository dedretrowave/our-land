using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components
{
    public class ScenesSwitcher : MonoBehaviour
    {
        public void OpenLevelScene()
        {
            SceneManager.LoadScene("Level");
        }
    }
}