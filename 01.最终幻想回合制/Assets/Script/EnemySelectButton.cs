using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;

    /// <summary>
    /// ѡ�����
    /// </summary>
    public void  SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMaschine>();
    }
}
