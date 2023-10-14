using System;
using UnityEngine;

public class Shield : MonoBehaviour, IBooster
{
    public event Action<IBooster> BoostCollected;

    public void SetParent(Transform parent) =>
        transform.SetParent(parent);

    public void SetPosition(Vector3 position) =>
        transform.localPosition = position;

    public void Inactive() => 
        gameObject.SetActive(false);

    public void CallInactive() => 
        BoostCollected?.Invoke(this);

    private void OnTriggerEnter2D(Collider2D other) =>
        CallInactive();
}