using UnityEngine;

namespace Src.Levels.Level.Progress
{
    public class LevelProgress : MonoBehaviour
    {
        public void Win()
        {
            Debug.Log("YOU WON");
        }

        public void Lose()
        {
            Debug.Log("YOU LOSE");
        }
    }
}