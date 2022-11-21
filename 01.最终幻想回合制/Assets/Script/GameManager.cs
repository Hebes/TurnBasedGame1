using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��һ��������WorldMap  ����ű������
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    /// <summary>
    /// ��������� CLASS RANDOM MONSTER
    /// </summary>
    [System.Serializable]
    public class RegionData
    {
        public string regionName;
        public int maxAmountEnemys = 4;
        public string BattleScene;
        public List<GameObject> possibleEnemys = new List<GameObject>();
    }
    /// <summary>
    /// ��������
    /// </summary>
    public int enemyAmount;
    /// <summary>
    /// BATTLE ս���Ĺ����б�
    /// </summary>
    public List<GameObject> enemysToBattle = new List<GameObject>();
    /// <summary>
    /// ��ǰ������
    /// </summary>
    public int curRegions;
    /// <summary>
    /// ��������б�
    /// </summary>
    public List<RegionData> Regions = new List<RegionData>();

    /// <summary>
    /// ������ SPAWNPOINTS
    /// </summary>
    public string nextSpawnPoint;

    /// <summary>
    /// ��ҽ�ɫԤ����
    /// </summary>
    public GameObject heroCharacter;

    /// <summary>
    /// POSITIONS ��һ��Ӣ�۵�λ��
    /// </summary>
    public Vector3 nextHeroPosition;
    /// <summary>
    /// Ӣ������λ��
    /// </summary>
    public Vector3 lastHeroPosition;

    //SCENES
    public string sceneToLoad;//��һ������������
    public string lastScene;//BATTLE ���һ������������

    /// <summary>
    /// B0OLS  �Ƿ��ƶ�
    /// </summary>
    public bool isWalking = false;
    /// <summary>
    /// ��������  ����ս��
    /// </summary>
    public bool canGetEncounter = false;
    /// <summary>
    /// ����ս��
    /// </summary>
    public bool gotAttacked = false;

    /// <summary>
    /// ��Ϸ״̬
    /// </summary>
    public enum GameStates
    {
        /// <summary>
        /// ����
        /// </summary>
        WORLD_STATE,
        /// <summary>
        /// ����
        /// </summary>
        TOWN_STATE,
        /// <summary>
        /// ս��
        /// </summary>
        BATTLE_STATE,
        /// <summary>
        /// ���õ�
        /// </summary>
        IDLE,
    }
    /// <summary>
    /// ��Ϸ״̬
    /// </summary>
    public GameStates gameState;

    private void Awake()
    {
        //��һ��������WorldMap ����ű������
        //check if instance exist
        if (instance == null)
            //if not set the instance to this .
            instance = this;
        //if it exist but is not this instance
        else if (instance != this)
            //destroy it
            Destroy(gameObject);
        //set this to be not destroyable
        DontDestroyOnLoad(gameObject);

        if (!GameObject.Find("HeroCharacter"))
        {
            GameObject Hero = Instantiate(heroCharacter, nextHeroPosition, Quaternion.identity);
            Hero.name = "HeroCharacter";
        }
    }
    private void Update()
    {
        switch (gameState)
        {
            case GameStates.WORLD_STATE:
                if (isWalking)
                    RandomEncounter();
                if (gotAttacked)
                    gameState = GameStates.BATTLE_STATE;
                break;
            case GameStates.TOWN_STATE:
                break;
            case GameStates.BATTLE_STATE:
                //����ս������
                StartBattle();
                //ȥ��IDLE״̬
                gameState = GameStates.IDLE;
                break;
            case GameStates.IDLE:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ������һ������
    /// </summary>
    public void LoadNextScene() => SceneManager.LoadScene(sceneToLoad);

    /// <summary>
    /// �������һ�γ���  ս����Ϻ�
    /// </summary>
    public void LoadSceneAfterBattle() => SceneManager.LoadScene(lastScene);


    /// <summary>
    /// �������
    /// </summary>
    void RandomEncounter()
    {
        if (isWalking && canGetEncounter)//�����ƶ������ǿ������ֵ�����
        {
            if (Random.Range(0, 1000) < 10)
            {
                Debug.Log("I got attacked");
                gotAttacked = true;
            }
        }
    }

    void StartBattle()
    {
        //AMOUNT OF ENEMYS
        enemyAmount = Random.Range(1, Regions[curRegions].maxAmountEnemys + 1);
        //WHICH ENEMYS
        for (int i = 0; i < enemyAmount; i++)
            enemysToBattle.Add(Regions[curRegions].possibleEnemys[Random.Range(0, Regions[curRegions].possibleEnemys.Count)]);
        //HERO
        lastHeroPosition = GameObject.Find("HeroCharacter").gameObject.transform.position;
        nextHeroPosition = lastHeroPosition;
        lastScene = SceneManager.GetActiveScene().name;
        //LOAD LEVEL
        SceneManager.LoadScene(Regions[curRegions].BattleScene);
        //RESET HERO
        isWalking = false;
        gotAttacked = false;
        canGetEncounter = false;
    }
}
