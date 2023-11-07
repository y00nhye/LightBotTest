
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{
    [SerializeField] Command[] playerCommandList; //Ŀ�ǵ� ��� �޾� ������ Ŭ���� ����Ʈ (������� ����)

    Command playerCommand;

    Vector3 playerStartPos;
    Vector3 playerStartRot;

    bool isReady = false;

    bool isMove = false;
    int moveMainCnt;
    int moveProc1Cnt;
    int moveProc2Cnt;

    private void OnEnable() //default �÷��̾� Ŀ�ǵ� ����
    {
        playerCommand = playerCommandList[1];
    }

    private void Update()
    {
        if (isMove)
        {
            if (isReady)
            {
                MainSimulation();
            }

            FinishCheck();

            isReady = playerCommand.isReady;
        }
    }

    private void MainSimulation() //���� �Լ��� ���Ե� �÷��̾� Ŀ�ǵ� ����
    {
        if (moveMainCnt == GameManager.Instance.playerInput[0].Count)
        {
            playerCommand.playerAni.SetBool("GO", false);
            playerCommand.playerAni.SetBool("JUMP", false);

            isMove = false;

            return;
        }

        UIManager.Instance.ResultBoardBtnOn(moveMainCnt, 0);

        if (GameManager.Instance.playerInput[0][moveMainCnt] == 5)
        {
            if (Proc1Simulation()) moveMainCnt++;
        }
        else if (GameManager.Instance.playerInput[0][moveMainCnt] == 6)
        {
            if (Proc2Simulation()) moveMainCnt++;
        }
        else
        {
            Movement(GameManager.Instance.playerInput[0][moveMainCnt]);
            moveMainCnt++;
        }
    }

    private bool Proc1Simulation() //Proc1 �Լ��� ���Ե� �÷��̾� Ŀ�ǵ� ����
    {
        if (GameManager.Instance.playerInput[1].Count == moveProc1Cnt)
        {
            moveProc1Cnt = 0;
            UIManager.Instance.ResultBoardBtnOff(1);
            playerCommand.isReady = true;
            return true;
        }

        UIManager.Instance.ResultBoardBtnOn(moveProc1Cnt, 1);

        if (GameManager.Instance.playerInput[1][moveProc1Cnt] == 5)
        {
            UIManager.Instance.ResultBoardBtnOff(1);
            moveProc1Cnt = 0;
        }
        else if (GameManager.Instance.playerInput[1][moveProc1Cnt] == 6)
        {
            if (Proc2Simulation()) moveProc1Cnt++;
        }
        else
        {
            Movement(GameManager.Instance.playerInput[1][moveProc1Cnt]);

            moveProc1Cnt++;
        }

        return false;
    }

    private bool Proc2Simulation() //Proc2 �Լ��� ���Ե� �÷��̾� Ŀ�ǵ� ����
    {
        if (GameManager.Instance.playerInput[2].Count == moveProc2Cnt)
        {
            moveProc2Cnt = 0;
            UIManager.Instance.ResultBoardBtnOff(2);
            playerCommand.isReady = true;
            return true;
        }

        UIManager.Instance.ResultBoardBtnOn(moveProc2Cnt, 2);

        if (GameManager.Instance.playerInput[2][moveProc2Cnt] == 6)
        {
            UIManager.Instance.ResultBoardBtnOff(2);
            moveProc2Cnt = 0;
        }
        else if (GameManager.Instance.playerInput[2][moveProc2Cnt] == 5)
        {
            if (Proc1Simulation()) moveProc2Cnt++;
        }
        else
        {
            Movement(GameManager.Instance.playerInput[2][moveProc2Cnt]);

            moveProc2Cnt++;
        }

        return false;
    }

    private void Movement(int playerMove) //�̵� ����
    {
        isReady = false;

        playerCommand = playerCommandList[playerMove];
        playerCommand.Action(); //�÷��̾� Ŀ�ǵ忡 ������ action �Լ� ����
    }

    public void StartMove() //������ ���� ���� ����
    {
        moveMainCnt = 0;
        moveProc1Cnt = 0;
        moveProc2Cnt = 0;

        if (GameManager.Instance.playerInput[0].Count != 0)
        {
            isMove = true;
            isReady = true;
        }
    }

    public void StopMove() //������ ���� ���� ����
    {
        playerCommand.playerAni.SetBool("GO", false);
        playerCommand.playerAni.SetBool("JUMP", false);
        playerCommand.StopAllCoroutines();

        GetComponent<Light_Command>().LightReset();

        isMove = false;

        transform.position = playerStartPos;
        transform.eulerAngles = playerStartRot;

        GameManager.Instance.currentLight = 0;

        UIManager.Instance.ResultBoardBtnOff(0);
        UIManager.Instance.ResultBoardBtnOff(1);
        UIManager.Instance.ResultBoardBtnOff(2);
    }

    private void FinishCheck() // ���� ���� Ȯ��
    {
        if (GameManager.Instance.currentLight == GameManager.Instance.roundInfo[GameManager.Instance.roundCnt - 1].lightCnt)
        {
            playerCommand.playerAni.SetBool("GO", false);
            playerCommand.playerAni.SetBool("JUMP", false);
            playerCommand.StopAllCoroutines();

            GameManager.Instance.isFinish = true;
        }
    }

    public void LoadPlayer() //�� �ε� �� ó�� �÷��̾� ��ȯ ����
    {
        GetComponent<Light_Command>().lightAni = null;

        transform.position = MapProducer.Instance.startPos;
        transform.eulerAngles = MapProducer.Instance.startAngle;

        playerStartPos = transform.position;
        playerStartRot = transform.eulerAngles;

        StartCoroutine(LoadPlayer_co());

        isMove = false;
    }

    IEnumerator LoadPlayer_co() //�÷��̾� ��ȯ �ڷ�ƾ
    {
        transform.position += Vector3.up;

        while (Vector3.Distance(transform.position, playerStartPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, playerStartPos, playerCommand.movespeed * 1.5f);

            yield return null;
        }

        transform.position = playerStartPos;

        UIManager.Instance.PlayerInputBtnOn();
    }
}
