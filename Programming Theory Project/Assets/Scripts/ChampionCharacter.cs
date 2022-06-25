using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Subclass of CharacterHandler
// INHERITANCE
public class ChampionCharacter : CharacterHandler
{
    // Start() and Update() methods deleted - we don't need them right now (probably will need Update for keypresses and stuff, unless it is gonna be done on a separate script)

    private Tower m_CurrentAttackingTarget;

    // POLYMORPHISM (4 Pillars of OOP)
    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        m_CurrentAttackingTarget = null;
    }

    // POLYMORPHISM (4 Pillars of OOP)
    protected override void TowerInRange()
    {
        //we arrive at the tower, attack!
        GoTo(m_CurrentAttackingTarget);
        //m_Target.Attack(); // Attack method needs to be created

        //throw new System.NotImplementedException();
    }
}
