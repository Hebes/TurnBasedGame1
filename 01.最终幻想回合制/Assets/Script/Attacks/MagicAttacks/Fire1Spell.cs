using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Fire1Spell : BaseAttack
{
    public Fire1Spell()
    {
        attackName = "Fire1";
        attackDescription = "Basic Fire Spe11 which burns nothing.";
        attackDamage = 20f;
        attackCost = 10f;
    }
}
