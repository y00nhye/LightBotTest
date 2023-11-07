using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public TextAsset data;
    public AllData datas;

    private void Awake()
    {
        datas = JsonUtility.FromJson<AllData>(data.text);
    }
}

[System.Serializable]
public class AllData
{
    public MapData[] round1;
    public MapData[] round2;

    public Dictionary<int, MapData> allRoundData;

    public void AllRoundDataInit()
    {

    }
}

[System.Serializable]
public class MapData
{
    public int height;
    public int baseNum;
    public int x;
    public int z;
    public int y;
    public float yAngle;
    public int type;
    public int isStart;
}
