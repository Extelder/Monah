using System;
using System.Collections;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitСharacteristics _characteristics;
    [field: SerializeField] public Point StartPoint { get; private set; }

    private Point _currentPoint;

    private bool _canChangePath;
    public float CollectedMoney;

    private void Start()
    {
        _currentPoint = StartPoint;
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        while (true)
        {
            MoveByPath(_currentPoint.GetRandomPath(this).EndPoint, () => OnPointReached());
            yield return new WaitUntil(() => _canChangePath);
        }
    }

    private IEnumerator CollectMoneyFromColony(Colony colony)
    {
        float collectedMoneyFromThisColony = 0;
        while (colony.CurrentMoney > 0)
        {
            if (CollectedMoney + _characteristics.CollectMoneyPerSecond > _characteristics.MaxMoney)
            {
                StartCoroutine(MovingToBase());
                _currentPoint.Colony.CanEarnMoney = true;
                yield break;
            }
            if (collectedMoneyFromThisColony + _characteristics.CollectMoneyPerSecond >
                _characteristics.MaxMoneyFromOneColony)
                break;
            collectedMoneyFromThisColony += _characteristics.CollectMoneyPerSecond;
            CollectedMoney += _characteristics.CollectMoneyPerSecond;
            colony.CurrentMoney -= _characteristics.CollectMoneyPerSecond;
            Debug.Log("Current collected money - " + CollectedMoney);
            Debug.Log("Current collected money from this colony - " + collectedMoneyFromThisColony);
            yield return new WaitForSeconds(1);
        }

        _currentPoint.Colony.CanEarnMoney = true;
        _canChangePath = true;
    }

    private IEnumerator MovingToBase()
    {
        bool pointReached = true;
        while (_currentPoint != StartPoint)
        {
            yield return new WaitUntil(() => pointReached);
            pointReached = false;
            MoveByPath(_currentPoint.FindClosestToPointPath(StartPoint).EndPoint, () => pointReached = true);
            if (_currentPoint.TryGetComponent<Hub>(out Hub hub))
            {
                hub.UnitEntered(this);
                _canChangePath = true;
                yield break;
            }
        }
    }
    
    private void MoveByPath(Point point, Action onPointReached)
    {
        _canChangePath = false;
        transform.DOMove(point.GetPosition(), _characteristics.Speed).OnComplete(() =>
        {
            _currentPoint = point;
            onPointReached?.Invoke();
        });
    }

    private void OnPointReached()
    {
        _currentPoint.UnitEntered?.Invoke(this); 
        _currentPoint.Colony.CanEarnMoney = false;
        StartCoroutine(CollectMoneyFromColony(_currentPoint.Colony));
    }

    public void SpendAllCollectedMoney()
    {
        CollectedMoney = 0;
        DataChanged();
    }
    
    public void DataChanged()
    {
        
    }
}