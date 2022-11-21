using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private float moveSpeed = 500f;

    private Vector3 curPos, LastPos;

    private void Start()
    {
        if (GameManager.instance.nextSpawnPoint != "")
        {
            GameObject spawnPoint = GameObject.Find(GameManager.instance.nextSpawnPoint);
            transform.position = spawnPoint.transform.position;
            GameManager.instance.nextSpawnPoint = "";
        }
        else if (GameManager.instance.lastHeroPosition != Vector3.zero)
        {
            transform.position = GameManager.instance.lastHeroPosition;
            GameManager.instance.lastHeroPosition = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);
        GetComponent<Rigidbody>().velocity = movement * moveSpeed * Time.fixedDeltaTime;

        curPos = transform.position;
        GameManager.instance.isWalking = curPos == LastPos ? false : true;
        LastPos = curPos;
    }

    void OnTriggerEnter(Collider other)
    {
        //���ɵ�
        if (other.tag == "teleporter")
        {
            CollisionHandler col = other.gameObject.GetComponent<CollisionHandler>();
            GameManager.instance.nextSpawnPoint = col.spawnPointName;   //���뷿�ݻᱨ������  ��Ϊû�� �������������ͼ������
            GameManager.instance.sceneToLoad = col.sceneToLoad;
            GameManager.instance.LoadNextScene();
        }

        if (other.tag == "region1")
            GameManager.instance.curRegions = 0;
        if (other.tag == "region2")
            GameManager.instance.curRegions = 1;
    }

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "region1" || other.tag == "region2")
        {
            GameManager.instance.canGetEncounter = true;
        }
    }
    /// <summary>
    /// �ر���������
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "region1" || other.tag == "region2")
        {
            GameManager.instance.canGetEncounter = false;
        }
    }

}
