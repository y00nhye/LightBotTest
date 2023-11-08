using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_Command : Command
{
    bool canGo;

    public override void Action()
    {
        isReady = false;
        canGo = false;
        tar = transform.position + transform.forward;
        playerAni.SetBool("GO", true);

        int tar_i = (int)Mathf.Round(tar.x) * 100
            + (int)Mathf.Round(tar.z) * 10
            + (int)Mathf.Round(tar.y); //��ǥ�� �ϳ��� int ������ ����

        if (MapLoader.Instance.baseData.ContainsKey(tar_i)) //containskey �� �����ϰ� ã��!
        {
            canGo = true;
        }
        
        /*foreach (int pos in MapLoader.Instance.baseData.Keys) //dictionary key ���� ��ǥ�� ��
        {
            if(tar_i == pos) //��ǥ�� base dictionary�� �ִٸ�
            {
                canGo = true;
                break;
            }
        }*/

        StartCoroutine(Action_co());
    }

    public override IEnumerator Action_co()
    {
        if (canGo)
        {
            while (Vector3.Distance(transform.position, tar) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tar, movespeed);

                yield return null;
            }
            transform.position = tar;
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

        isReady = true;

        yield return new WaitForSeconds(1f);
    }
}
