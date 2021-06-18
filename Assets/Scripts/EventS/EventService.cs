using UnityEngine;
using System;

public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action onEnemyDeath;
    public event Action onFire;

    
    public void InvokeEnemyKilledEvent()
    {
        onEnemyDeath?.Invoke();
    }

    public void InvokeFireEvent()
    {
        onFire?.Invoke();
    }

}