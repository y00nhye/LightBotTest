using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_Command : Command
{
    Vector3 tarU;
    Vector3 tarD;

    Vector3 tarMax;

    bool canJump;

    public override void Action()
    {   
        isReady = false;
        canJump = false;

        tarU = transform.position + transform.forward + transform.up;
        tarD = transform.position + transform.forward - transform.up;

        int tarU_i = (int)tarU.x * 100 + (int)tarU.z * 10 + (int)tarU.y; //xzy 하나의 값
        int tarD_i = (int)tarD.x * 100 + (int)tarD.z * 10 + (int)tarD.y; //xzy 하나의 값

        foreach (int pos in MapProducer.Instance.baseData.Keys)
        {
            canJump = true;
            if (tarU_i == pos)
            {
                canJump = true;
                tar = tarU;
                tarMax = transform.position + transform.forward * 0.5f + transform.up * 1.3f;
                break;
            }
            else if(tarD_i == pos)
            {
                canJump = true;
                tar = tarD;
                tarMax = transform.position + transform.forward * 0.5f + transform.up * 0.5f;
                break;
            }
        }

        playerAni.SetBool("GO", false);
        playerAni.SetBool("JUMP", true);

        StartCoroutine(Action_co());
    }

    public override IEnumerator Action_co()
    {
        if (canJump)
        {
            while (Vector3.Distance(transform.position, tarMax) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tarMax, movespeed);

                yield return null;
            }
            transform.position = tarMax;

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

        playerAni.SetBool("JUMP", false);
        isReady = true;

        yield return new WaitForSeconds(1f);
    }
}
