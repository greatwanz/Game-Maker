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

        public void UpdateScore(int scoreToAdd)
        {
            _score.Add(scoreToAdd);
            _scoreText.text = $"Score: {_score.Value}";
        }
    }
}
