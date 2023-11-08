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

        int tar_i = (int)Mathf.Round(tar.x) * 100 
            + (int)Mathf.Round(tar.z) * 10 
            + (int)Mathf.Round(tar.y);

        foreach (int pos in MapLoader.Instance.baseData.Keys)
        {
            if (tar_i == pos)
            {
                if (MapLoader.Instance.baseData[pos].type == 1) //해당 data에서 type이 1이라면 (light block)
                {
                    isLight = true;

                    //base 오브젝트에 접근해 animator 가져오기
                    lightAni = MapLoader.Instance.bases
                        [int.Parse(MapLoader.Instance.baseData[pos].baseNum)].GetComponent<Animator>();
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
