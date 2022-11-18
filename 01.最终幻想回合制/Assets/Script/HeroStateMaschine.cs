using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ӣ��״̬��
/// </summary>
public class HeroStateMaschine : MonoBehaviour
{
    private BattleStateMaschine BSM;
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
    public GameObject Selector;

    //Ӣ�ۻ���
    public GameObject EnemyToAttack;
    private bool actionStarted = false;
    public Vector3 startPosition;
    private float animSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        cur_colldown = Random.Range(0, 2.5f);
        Selector.SetActive(false);
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMaschine>();
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
                BSM.HeroToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;
            case TurnState.WAITING:
                //����״̬
                break;
            case TurnState.SELECTING:
                break;
            case TurnState.ACTION:
                StartCoroutine(TimeForAction());
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

    /// <summary>
    /// �ж���ʱ�䵽��
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;//���û�������ж�,ֱ������Э��
        }
        actionStarted = true;
        //���ŵ��˽ӽ�Ӣ�۵Ĺ�������
        Vector3 enemyPostion = new Vector3(
            EnemyToAttack.transform.position.x + 1.5f,
            EnemyToAttack.transform.position.y,
            EnemyToAttack.transform.position.z);
        while (MoveTowrdsEnemy(enemyPostion))//ѭ���ȴ�1֡
            yield return null;//����ǵȴ�1֡����˼
        //�ȴ�
        yield return new WaitForSeconds(0.5f);
        //�˺�
        //�ص���ʼλ�õĶ���
        Vector3 firstPosition = startPosition;
        while (MoveTowrdsStart(firstPosition))//ѭ���ȴ�1֡
            yield return null;//����ǵȴ�1֡����˼
        //��BSM��Performer�б��Ƴ�
        BSM.PerformList.RemoveAt(0);
        //����BSM->�ȴ�
        BSM.battleState = BattleStateMaschine.PerfromAction.WAIT;
        //����Э��
        actionStarted = false;
        //���õ���״̬
        cur_colldown = 0f;
        currentState = TurnState.PROCESSING;
    }

    /// <summary>
    /// �ƶ����� �������û�ƶ�����������ʱ��  ���صľ���false
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool MoveTowrdsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    /// <summary>
    /// �ص�ԭ����λ��
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool MoveTowrdsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
}
