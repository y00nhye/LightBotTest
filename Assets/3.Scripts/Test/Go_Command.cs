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
            + (int)Mathf.Round(tar.y); //좌표를 하나의 int 값으로 변경

        if (MapLoader.Instance.baseData.ContainsKey(tar_i)) //containskey 로 간편하게 찾기!
        {
            canGo = true;
        }
        
        /*foreach (int pos in MapLoader.Instance.baseData.Keys) //dictionary key 값과 좌표값 비교
        {
            if(tar_i == pos) //좌표가 base dictionary에 있다면
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
