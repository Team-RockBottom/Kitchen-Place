using System;
using UnityEngine;

public class UIStateController : MonoBehaviour
{
    UIState currentUIState;
    private enum UIState
    {
        nomal,
        objectselected,
        moveMode,
        rotateMode,
        scaleMode
    }


    private void Update()
    {

    }

    void OnButton()
    {
        switch (currentUIState)
        {
            case UIState.nomal:
                break;
            case UIState.objectselected:
                break;
            case UIState.moveMode:
                break;
            case UIState.rotateMode:
                break;
            case UIState.scaleMode:
                break;
        }
    }

    //void StateNomal()
    //{
    //    //���α׷� ���� Ȯ��
    //}

    //void StateObjectSelected()
    //{
    //    //���°� ������Ʈ �����̸� nomal���·� ���ư���

    //}

    //void StateMoveMode()
    //{

    //}

    //void StateRotateMode()
    //{

    //}

    //void StateScaleMode()
    //{

    //}
}
