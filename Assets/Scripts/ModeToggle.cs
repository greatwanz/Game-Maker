using UnityEngine;

namespace Greatwanz.GameMaker
{
    public class ModeToggle : MonoBehaviour
    {
        [Header("Game Events")]
        [SerializeField] private BoolGameEvent _onPlaymodeToggleEvent;
        [Header("References")]
        [SerializeField] private UnityEngine.UI.Text _modeText;
        [Header("Text")]
        [SerializeField] private string _playString;
        [SerializeField] private string _createString;

        private bool _isPlaying;

        private void Awake()
        {
            _isPlaying = false;
            _modeText.text = _playString;
        }

        private void Start()
        {
            _onPlaymodeToggleEvent.Raise(false);
        }

        public void OnClick()
        {
            _isPlaying = !_isPlaying;
            _modeText.text = _isPlaying ? _createString : _playString;
            _onPlaymodeToggleEvent.Raise(_isPlaying);
        }
    }
}
