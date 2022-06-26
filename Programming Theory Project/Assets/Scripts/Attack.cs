using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { None, Pushback, Rockets, Smash }
public class Attack : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now
    public AttackType attackType;
}
