using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapProducer : Singleton<MapProducer>
{
    public GameObject map_base;
    public GameObject map_light;
    public Transform mapPos;

    public List<GameObject> allBases = new List<GameObject>();
    public List<GameObject> bases = new List<GameObject>();

    public Vector3 startPos;
    public Vector3 startAngle;

    public MapData[] currentMapData;
    public Dictionary<int, MapData> baseData = new Dictionary<int, MapData>();

    public Camera mainCam;

    private void MapReset()
    {
        for(int i = 0; i < allBases.Count; i++)
        {
            Destroy(allBases[i]);
        }

        bases = new List<GameObject>();
        allBases = new List<GameObject>();
        baseData = new Dictionary<int, MapData>();
        currentMapData = null;
    }

    public void RoundCheck()
    {
        MapReset();

        foreach (int key in DataManager.Instance.allRoundData.Keys)
        {
            if (key == GameManager.Instance.roundCnt)
            {
                currentMapData = DataManager.Instance.allRoundData[key];
            }
        }

        for (int i = 0; i < currentMapData.Length; i++)
        {
            int basePos_i = currentMapData[i].x * 100 + currentMapData[i].z * 10 + currentMapData[i].y;

            baseData.Add(basePos_i, currentMapData[i]);
        }
    }

    public void MapLoad()
    {
        RoundCheck();

        for (int i = 0; i < currentMapData.Length; i++)
        {
            Vector3 basePos;
            GameObject obj;

            for (int j = 0; j < currentMapData[i].y; j++)
            {
                if (j < currentMapData[i].y - 1)
                {
                    obj = Instantiate(map_base, mapPos);
                    basePos = new Vector3(currentMapData[i].x, j + 1, currentMapData[i].z);
                }
                else
                {
                    if (currentMapData[i].type == 0)
                    {
                        obj = Instantiate(map_base, mapPos);
                        basePos = new Vector3(currentMapData[i].x, j + 1, currentMapData[i].z);
                    }
                    else
                    {
                        obj = Instantiate(map_light, mapPos);
                        basePos = new Vector3(currentMapData[i].x, j + 1 - 0.1f, currentMapData[i].z);
                    }

                    if (currentMapData[i].isStart == 1)
                    {
                        startPos = new Vector3(currentMapData[i].x, j + 1, currentMapData[i].z); ;
                        startAngle = new Vector3(0, currentMapData[i].yAngle, 0);
                    }

                    bases.Add(obj);
                }
                obj.transform.position = basePos;

                allBases.Add(obj);
            }
        }

        mainCam.transform.position = new Vector3(
            mainCam.transform.position.x,
            mainCam.transform.position.y + currentMapData[0].height,
            mainCam.transform.position.z);
    }
}
