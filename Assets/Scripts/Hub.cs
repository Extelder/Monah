using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    [SerializeField] private float _money;
    
    public void UnitEntered(Unit unit)
    {
        GrabMoneyFromUnit(unit);
        Debug.Log("Hub Money - " + _money);
    }

    private void GrabMoneyFromUnit(Unit unit)
    {
        _money += unit.CollectedMoney;
        unit.SpendAllCollectedMoney();
    }
}
