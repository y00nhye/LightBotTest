using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ ������ �� ����ϴ� ���� ��Ҹ� ���� �θ� Ŭ����
public abstract class Command : MonoBehaviour
{
    public float movespeed = 0.005f; //������ �ӵ�
    public Animator playerAni;

    //������ �� �ٲ�� ��
    public Vector3 tar;
    public bool isReady;

    public abstract void Action(); //������ ���� �Լ�

    public abstract IEnumerator Action_co(); //������ �ڷ�ƾ
}
