using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public static event Action<float, float> HealthChanged;

    [SerializeField] private float _maxHealth;

    private float _currentHealth = 50;

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void IncreaseHealth()
    {
        float increasement = 5f;

        _currentHealth += increasement;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void DecreaseHealth()
    {
        float decreseament = 5f;

        _currentHealth -= decreseament;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
