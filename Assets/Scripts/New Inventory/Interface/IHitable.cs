using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    public void TakeDamage(int damage, Vector3 pointHit);
    public int CurrentHealth();
    public int MaxHealth();
}
