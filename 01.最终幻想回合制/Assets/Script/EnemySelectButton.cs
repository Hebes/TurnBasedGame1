using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SelectEnemy);
    }

    /// <summary>
    /// ѡ����� ק�е��Լ��İ�ť��
    /// </summary>
    public void  SelectEnemy()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMaschine>().Input2(EnemyPrefab);
    }
}
