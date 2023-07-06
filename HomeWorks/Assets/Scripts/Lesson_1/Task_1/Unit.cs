using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _currentHealth;
    private float _maxHealth = 100f;
    private float _stepToHealing = 5f;
    private float _maxHealthToHeling;

    private float _healingTime = 3f;
    private float _cooldownHealing = 0.5f;

    private bool _coroutineIsActivated = false;

    private void Awake()
    {
        _maxHealthToHeling = _maxHealth - _stepToHealing;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(_coroutineIsActivated == false)
            {
                ReceiveHealing();
            }
            else
            {
                Debug.Log("Healing system is started");
            }
        }
    }

    public void ReceiveHealing()
    {
        if (_currentHealth < _maxHealth)
        {
            _coroutineIsActivated = true;
            StartCoroutine(Healing());
        }
        else
        {
            Debug.Log("Health is full");
        }
    }

    IEnumerator Healing()
    {
        float fullTime = _healingTime;
        while (fullTime > 0)
        {

            if (_currentHealth >= _maxHealthToHeling)
            {
                _currentHealth = _maxHealth;
                fullTime = 0;
                Debug.Log($"Health {_currentHealth}");
                yield return null;
            }
            else
            {
                _currentHealth = _currentHealth + _stepToHealing;
                fullTime = fullTime - _cooldownHealing;
                Debug.Log($"Health {_currentHealth}");
                yield return new WaitForSeconds(_cooldownHealing);
            }
        }
        _coroutineIsActivated = false;

        yield return null;
    }
}