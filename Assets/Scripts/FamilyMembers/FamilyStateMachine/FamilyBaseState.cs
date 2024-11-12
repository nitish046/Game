using UnityEditor;
using UnityEngine;

public abstract class FamilyBaseState
{
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();
}
