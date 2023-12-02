using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Temp
{
    public sealed class EffectsListViewController : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private EffectView _prefab;

        private readonly Dictionary<Effect, EffectView> _views = new();

        private EffectPresenter _effectPresenter;

        private void Awake()
        {
            Install(new EffectPresenter(new EffectCollection(), this));
        }

        public void Install(EffectPresenter effectPresenter)
        {
            _effectPresenter = effectPresenter;
        }

        private void OnEnable()
        {
            _effectPresenter.Subscribe();
        }

        private void OnDisable()
        {
            _effectPresenter.Unsubscribe();
        }

        public void AddEffect(Effect effect)
        {
            var view = Instantiate(_prefab, _parent);
            view.Init(effect);
            _views.Add(effect, view);
        }

        public void RemoveEffect(Effect effect)
        {
            var effectView = _views[effect];
            _views.Remove(effect);
            Destroy(effectView);
        }
    }

    public sealed class EffectView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshPro _title;
        [SerializeField] private Image _backGround;

        private Effect _effect;

        public void Init(Effect effect)
        {
            _effect = effect;

            _icon.sprite = effect.Icon;
            _title.text = effect.Value.ToString(CultureInfo.InvariantCulture);
            _backGround.color = effect.Color;

            _effect.OnValueChanged += UpdateValue;
        }

        private void OnDisable()
        {
            _effect.OnValueChanged -= UpdateValue;
        }

        private void UpdateValue(float value)
        {
            _title.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public sealed class EffectPresenter
    {
        private readonly EffectCollection _effectCollection;
        private readonly EffectsListViewController _effectsListViewController;

        public EffectPresenter(EffectCollection effectCollection, EffectsListViewController effectsListViewController)
        {
            _effectCollection = effectCollection;
            _effectsListViewController = effectsListViewController;
        }

        public void Subscribe()
        {
            _effectCollection.OnAdded += _effectsListViewController.AddEffect;
            _effectCollection.OnRemoved += _effectsListViewController.RemoveEffect;
        }

        public void Unsubscribe()
        {
            _effectCollection.OnAdded -= _effectsListViewController.AddEffect;
            _effectCollection.OnRemoved -= _effectsListViewController.RemoveEffect;
        }
    }

    public sealed class Effect
    {
        public event Action<float> OnValueChanged;

        public float Value { get; private set; }
        public Sprite Icon { get; }
        public Color Color { get; }

        public void SetValue(float value)
        {
            Value = value;
            OnValueChanged?.Invoke(value);
        }

        public Effect(float value, Sprite icon, Color color)
        {
            Value = value;
            Icon = icon;
            Color = color;
        }
    }

    public sealed class EffectCollection
    {
        public event Action<Effect> OnAdded;
        public event Action<Effect> OnRemoved;

        public int Count => this.effects.Count;

        private readonly HashSet<Effect> effects = new();

        public void AddEffect(Effect effect)
        {
            if (this.effects.Add(effect))
            {
                this.OnAdded?.Invoke(effect);
            }
        }

        public void RemoveEffect(Effect effect)
        {
            if (this.effects.Remove(effect))
            {
                this.OnRemoved?.Invoke(effect);
            }
        }

        public IEnumerable<Effect> GetEffects()
        {
            return this.effects;
        }
    }
}