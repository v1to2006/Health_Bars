using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderHealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillRectImage;
    [SerializeField] private Color _fillColor;
    [SerializeField] private Color _emptyColor;
    [SerializeField] private float _transitionSpeed;

    private Coroutine _healthTransitionCoroutine = null;

    private void OnEnable()
    {
        Health.HealthChanged += DisplayHealth;
    }

    private void OnDisable()
    {
        Health.HealthChanged -= DisplayHealth;
    }

    private void DisplayHealth(float currentHealth, float maxHealth)
    {
        _slider.minValue = 0;
        _slider.maxValue = maxHealth;

        if (_healthTransitionCoroutine != null)
            StopCoroutine(_healthTransitionCoroutine);

        _healthTransitionCoroutine = StartCoroutine(TransitionHealth(currentHealth));
    }

    private IEnumerator TransitionHealth(float currentHealth)
    {
        while (_slider != null && _slider.value != currentHealth)
        {
            if (currentHealth <= 0)
            {
                if (_fillRectImage != null)
                {
                    _fillRectImage.color = _emptyColor;
                }
            }
            else
            {
                if (_fillRectImage != null)
                {
                    _fillRectImage.color = _fillColor;
                }
            }

            if (_slider != null)
            {
                _slider.value = Mathf.Lerp(_slider.value, currentHealth, _transitionSpeed * Time.deltaTime);
            }

            yield return null;
        }
    }

    private void OnDestroy()
    {
        if (_healthTransitionCoroutine != null)
            StopCoroutine(_healthTransitionCoroutine);
    }
}
