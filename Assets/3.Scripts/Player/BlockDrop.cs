using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockDrop : MonoBehaviour, IPointerEnterHandler //���콺 ���� ���¿� ���� ��� ���� ���� ���� Ŭ����
{
    public int blockNum;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ResultBoardClick(blockNum);
    }
}
