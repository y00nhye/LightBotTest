using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ ������ �� ����ϴ� ���� ��Ҹ� ���� �θ� Ŭ����
public abstract class Command : MonoBehaviour
{
    public float movespeed = 0.005f; //������ �ӵ�
    public Animator playerAni;

    //������ �� �ٲ�� ��
    public bool canGo;
    public bool canJump;
    public Vector3 tar;
    public bool isReady;

    public abstract void Action(bool isWall, bool isJump, bool isJumpDown); //������ ���� �Լ�

    public abstract IEnumerator Action_co(); //������ �ڷ�ƾ
}
