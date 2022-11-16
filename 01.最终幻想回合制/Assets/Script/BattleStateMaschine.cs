using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ս��״̬��
/// </summary>
public class BattleStateMaschine : MonoBehaviour
{
    /// <summary>
    /// ִ�в���
    /// </summary>
    public enum PerfromAction
    {
        /// <summary>
        /// �ȴ�
        /// </summary>
        WAIT,
        /// <summary>
        /// ��ȡ�ж�
        /// </summary>
        TAKEACTION,
        /// <summary>
        /// ִ�ж���
        /// </summary>
        PERFROMACTION,
    }

    /// <summary>
    /// ս����״̬
    /// </summary>
    public PerfromAction battleState;

    public List<HandleTurn> PerformList = new List<HandleTurn>();
    public List<GameObject> HerosInBattle = new List<GameObject>();
    public List<GameObject> EnemyssInBattle = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        battleState = PerfromAction.WAIT;
        EnemyssInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleState)
        {
            case PerfromAction.WAIT:
                break;
            case PerfromAction.TAKEACTION:
                break;
            case PerfromAction.PERFROMACTION:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// �Ѽ��ж�
    /// </summary>
    public void CollectActions(HandleTurn input)
    {
        PerformList.Add(input);
    }
}
