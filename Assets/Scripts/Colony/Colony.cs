using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    [field: SerializeField] public float CurrentMoney { get; set; }
    [field: SerializeField] public float MaxMoney { get; private set; }
    [field: SerializeField] public float MoneyRate { get; private set; }
    [field: SerializeField] public float EarnMoneyPerRate { get; private set; }

    public bool CanEarnMoney = true;
    
    private void Start()
    {
        StartCoroutine(EarnMoney());
    }

    private IEnumerator EarnMoney()
    {
        while (true)
        {
            yield return new WaitUntil(() => CurrentMoney + MoneyRate < MaxMoney && CanEarnMoney);
            CurrentMoney += EarnMoneyPerRate;
            if (CurrentMoney > MaxMoney)
                CurrentMoney = MaxMoney;
            yield return new WaitForSeconds(MoneyRate);
        }
    }

    private IEnumerator SpendMoney(float spendRate, float spendPerRate)
    {
        while (CurrentMoney > 0)
        {
            CurrentMoney -= spendPerRate;
            yield return new WaitForSeconds(spendRate);
        }
    }
}