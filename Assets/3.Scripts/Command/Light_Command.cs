using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Command : Command
{
    public Animator lightAni;

    public List<Animator> lightAniOn = new List<Animator>();

    bool isLight;

    public override void Action()
    {
        isReady = false;
        isLight = false;
        tar = transform.position;
        playerAni.SetBool("GO", false);
        playerAni.SetBool("JUMP", false);

        Debug.Log(tar);

        int tar_i = (int)tar.x * 100 + (int)tar.z * 10 + (int)tar.y; //xzy 하나의 값

        Debug.Log(tar_i);

        foreach (int pos in MapProducer.Instance.baseData.Keys)
        {
            if (tar_i == pos)
            {
                if (MapProducer.Instance.baseData[pos].type == 1)
                {
                    isLight = true;

                    lightAni = MapProducer.Instance.bases
                        [int.Parse(MapProducer.Instance.baseData[pos].baseNum)].GetComponent<Animator>();
                }

                break;
            }
        }

        StartCoroutine(Action_co());
    }

    public override IEnumerator Action_co()
    {
        if (isLight)
        {
            if (!lightAni.GetComponent<BaseLInfo>().isOn) //light 켜기
            {
                lightAni.SetBool("LIGHT", true);
                lightAniOn.Add(lightAni);

                lightAni.GetComponent<BaseLInfo>().isOn = true;
                GameManager.Instance.currentLight++;

                yield return new WaitForSeconds(1f);
            }
            else
            {
                lightAni.SetBool("LIGHT", false); //light 끄기
                lightAniOn.Remove(lightAni);

                lightAni.GetComponent<BaseLInfo>().isOn = false;
                GameManager.Instance.currentLight--;

                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        isReady = true;
    }

    public void LightReset()
    {
        for (int i = 0; i < lightAniOn.Count; i++)
        {
            lightAniOn[i].SetBool("LIGHT", false);
            lightAniOn[i].GetComponent<BaseLInfo>().isOn = false;
        }

        lightAniOn = new List<Animator>();
    }
}
