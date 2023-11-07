using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProducer : MonoBehaviour
{
    public GameObject map_base;
    public GameObject map_light;
    public Transform mapPos;

    public void RoundCheck()
    {
        //for(int i = 0; i < DataManager.Instance.datas.)
    }

    public void MapLoad()
    {
        for (int i = 0; i < DataManager.Instance.datas.round2.Length; i++)
        {
            Vector3 basePos;
            GameObject obj;

            for (int j = 0; j < DataManager.Instance.datas.round2[i].y; j++)
            {
                if (j < DataManager.Instance.datas.round2[i].y - 1)
                {
                    obj = Instantiate(map_base, mapPos);
                    basePos = new Vector3(DataManager.Instance.datas.round2[i].x, j + 1, DataManager.Instance.datas.round2[i].z);
                }
                else
                {
                    if (DataManager.Instance.datas.round2[i].type == 0)
                    {
                        obj = Instantiate(map_base, mapPos);
                        basePos = new Vector3(DataManager.Instance.datas.round2[i].x, j + 1, DataManager.Instance.datas.round2[i].z);
                    }
                    else
                    {
                        obj = Instantiate(map_light, mapPos);
                        basePos = new Vector3(DataManager.Instance.datas.round2[i].x, j + 1 - 0.1f, DataManager.Instance.datas.round2[i].z);
                    }

                    if (DataManager.Instance.datas.round2[i].isStart == 1)
                    {
                        obj.name = "Base_start";
                        obj.transform.eulerAngles = new Vector3(0, DataManager.Instance.datas.round2[i].yAngle, 0);
                    }
                }

                obj.transform.position = basePos;
            }
        }

        mapPos.position = new Vector3(0, -DataManager.Instance.datas.round2[0].height, 0);
    }
}
