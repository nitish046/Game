using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilyStateMachine : FamilyStateMachine
{
    public EmilyChargeState charge_state;

    public EmilyStateMachine(FamilyPatrolState patrol, 
        FamilyBaseState activated, 
        FamilyFreezeState freeze, 
        FamilySearchState search, 
        EmilyChargeState charge)
        : base(patrol, activated, freeze, search)
    {
        charge_state = charge;
    }
}
