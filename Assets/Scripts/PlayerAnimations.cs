using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnAttack;

    public void AttackPerformed()
    {
        OnAttack.Invoke();
    }
    
}
