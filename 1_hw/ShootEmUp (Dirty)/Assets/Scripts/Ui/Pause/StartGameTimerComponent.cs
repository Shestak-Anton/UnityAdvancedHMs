using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class StartGameTimerComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private int _countdown = 3;
        
        private Sequence _counterSequence;

        public void StartTimer(Action onCountDown)
        {
            _counterSequence?.Kill();
            _counterSequence = DOTween.Sequence();
            for (var i = _countdown; i > 0; i--)
            {
                AppendTickAnimation(i);
            }

            _counterSequence.Append(_timerText.DOFade(0f, 0f));
            _counterSequence.OnComplete(() => { onCountDown?.Invoke(); });
        }

        private void AppendTickAnimation(int count)
        {
            _counterSequence.Append(_timerText.DOFade(0f, 0f));
            _counterSequence.AppendCallback(() => { _timerText.text = count.ToString(); });
            _counterSequence.Append(_timerText.DOFade(1f, 1f));
        }
    }
}