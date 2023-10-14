using System;
using UnityEngine;

public class RoadCrack : MonoBehaviour, IObstacles
{
    public event Action<IObstacles> ObstacleCollected;

    public void SetParent(Transform parent) => 
        transform.SetParent(parent);

    public void SetPosition(Vector3 position) => 
        transform.localPosition = position;

    public void Inactive() => 
        gameObject.SetActive(false);

    public void CallInactive() =>
        ObstacleCollected?.Invoke(this);
    
    private void OnTriggerEnter2D(Collider2D other) =>
        CallInactive();
}