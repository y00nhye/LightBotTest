using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : Singleton<MapLoader>
{
    //�� ���� ������Ʈ
    public GameObject map_base;
    public GameObject map_light;
    public Transform mapPos;

    public List<GameObject> allBases = new List<GameObject>(); //��� ������ �� ������Ʈ
    public List<GameObject> bases = new List<GameObject>(); //�÷��̾ ��� �� ������Ʈ (baseNum ������� add)

    //�÷��̾� ���� ��ġ
    public Vector3 startPos;
    public Vector3 startAngle;

    public MapData[] currentMapData; //���� �� ������
    public Dictionary<int, MapData> baseData = new Dictionary<int, MapData>(); //�� ��ǥ ������ (�̵� �� �ʿ�)

    public Camera mainCam;

    private void MapReset() //�� ����
    {
        for (int i = 0; i < allBases.Count; i++)
        {
            Destroy(allBases[i]);
        }

        bases = new List<GameObject>();
        allBases = new List<GameObject>();
        baseData = new Dictionary<int, MapData>();
        currentMapData = null;
    }

    public void RoundCheck() //���� �� ������ ��� ���� üũ
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

    public void MapLoad() //�� ����
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

                basePos = new Vector3(basePos.x, basePos.y - 10f, basePos.z);

                obj.transform.position = basePos;

                StartCoroutine(MapLoad_co(obj));

                allBases.Add(obj);
            }
        }

        mainCam.transform.position = new Vector3(
            mainCam.transform.position.x,
            mainCam.transform.position.y + currentMapData[0].height,
            mainCam.transform.position.z);
    }

    IEnumerator MapLoad_co(GameObject obj) //�� ���� ȿ��
    {
        float delay = Random.Range(0.0f, 1.0f);
        yield return new WaitForSeconds(delay); //�� ���� ������ �ֱ�

        Vector3 tar = new Vector3(obj.transform.position.x, obj.transform.position.y + 10f, obj.transform.position.z);

        while (Vector3.Distance(obj.transform.position, tar) > 0.01f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, tar, 0.2f);
            yield return null;
        }

        obj.transform.position = tar;
    }
}
