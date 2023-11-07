using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput> //�÷��̾� �Է� command ���� �Լ� ���� Ŭ����
{
    public void InputAdd(int input) //�߰�
    {
        if (UIManager.Instance.selectBoradInput.Length > GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Count)
        {
            GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Add(input);
            UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
        }

    }

    public void InputInsert(int index, int input) //����
    {
        if(UIManager.Instance.selectBoradInput.Length <= GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Count)
        {
            GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].RemoveAt(UIManager.Instance.selectBoradInput.Length - 1);
        }
        
        GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].Insert(index, input);

        UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
    }

    public void InputDelete(int index) //����
    {
        GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum].RemoveAt(index);

        UIManager.Instance.ResultBoardSet(GameManager.Instance.playerInput[UIManager.Instance.selectBoardNum]);
    }

    public void InputReset() //����
    {
        GameManager.Instance.playerInput[0] = new List<int>();
        GameManager.Instance.playerInput[1] = new List<int>();
    }
}
