using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public BaseAttack magicAttackToPerform;

    /// <summary>
    /// ʩչħ������
    /// </summary>
    public void CastMagicAttack()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMaschine>().Input4(magicAttackToPerform);
    }
}
