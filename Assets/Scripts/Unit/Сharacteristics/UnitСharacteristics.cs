using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitCharacteristics", menuName = "Unit/Characteristics")]
public class UnitСharacteristics : ScriptableObject
{
    [Header("Base characteristics")] 
    public float Speed;
    public float MaxMoney;
    public float MaxMoneyFromOneColony;
    public float CollectMoneyPerSecond;
}