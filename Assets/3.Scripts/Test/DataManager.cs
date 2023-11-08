using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public TextAsset data;
    public AllData datas;

    public Dictionary<int, MapData[]> allRoundData = new Dictionary<int, MapData[]>(); //¸Ê º°·Î data »ðÀÔ

    private void Awake()
    {
        datas = JsonUtility.FromJson<AllData>(data.text);

        DataInit();
    }

    private void DataInit()
    {
        allRoundData.Add(1, datas.round1);
        allRoundData.Add(2, datas.round2);
    }
}

[System.Serializable]
public class AllData
{
    public MapData[] round1;
    public MapData[] round2;
}

[System.Serializable]
public class MapData
{
    public int height;
    public string baseNum;
    public int x;
    public int z;
    public int y;
    public float yAngle;
    public int type;
    public int isStart;
}
