using UnityEngine;
using UnityEngine.UI;

namespace Greatwanz.GameMaker
{
    public class Scoreboard : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Text _scoreText;
        [Header("References")]
        [SerializeField] private IntVariable _score;

        private void Start()
        {
            _score.Set(0);
        }

        public void UpdateScore(int scoreToAdd)
        {
            _score.Set(_score.value + scoreToAdd);
            _scoreText.text = $"Score: {_score.value}";
        }
    }
}
