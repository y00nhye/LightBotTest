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

        int tar_i = (int)tar.x * 100 + (int)tar.z * 10 + (int)tar.y; //xzy 하나의 값

        foreach(int pos in MapProducer.Instance.baseData.Keys)
        {
            if(tar_i == pos)
            {
                canGo = true;
                break;
            }
        }

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
