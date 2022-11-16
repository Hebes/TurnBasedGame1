using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ӣ��״̬��
/// </summary>
public class HeroStateMaschine : MonoBehaviour
{
    public BaseHero baseHero;

    public enum TurnState
    {
        /// <summary>
        /// ����������
        /// </summary>
        PROCESSING,
        /// <summary>
        /// ��ӵ��б���
        /// </summary>
        ADDTOLIST,
        /// <summary>
        /// �ȴ�
        /// </summary>
        WAITING,
        /// <summary>
        /// ѡ��
        /// </summary>
        SELECTING,
        /// <summary>
        /// �ж�
        /// </summary>
        ACTION,
        /// <summary>
        /// ��ȥ��
        /// </summary>
        DEAD,
    }

    public TurnState currentState;
    public float cur_colldown = 0f;
    public float max_colldown = 5f;
    /// <summary>
    /// ��ȴ�������
    /// </summary>
    public Image ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        currentState = TurnState.PROCESSING;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Ӣ����ʾ��ǰ״̬:" + currentState);
        switch (currentState)
        {
            case TurnState.PROCESSING:
                UpgradeProgressBar();
                break;
            case TurnState.ADDTOLIST:
                break;
            case TurnState.WAITING:
                break;
            case TurnState.SELECTING:
                break;
            case TurnState.ACTION:
                break;
            case TurnState.DEAD:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ����������  ��ȴ��
    /// </summary>
    void UpgradeProgressBar()
    {
        cur_colldown = cur_colldown + Time.deltaTime;
        float calc_cooldown = cur_colldown / max_colldown;
        // ������ֵ�����ڸ�������С����ֵ����󸡵�ֵ֮�䡣
        // �������ֵ����Сֵ�����ֵ��Χ�ڣ��򷵻ظ���ֵ��
        ProgressBar.transform.localScale = new Vector3(
            Mathf.Clamp(calc_cooldown, 0, 1),
            ProgressBar.transform.localScale.y,
            ProgressBar.transform.localScale.z);
        if (cur_colldown >= max_colldown)//�����ȴʱ�䵽��
        {
            currentState = TurnState.ADDTOLIST;
        }
    }
}
