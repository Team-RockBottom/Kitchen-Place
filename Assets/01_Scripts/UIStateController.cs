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
    //    //프로그램 종료 확인
    //}

    //void StateObjectSelected()
    //{
    //    //상태가 오브젝트 선택이면 nomal상태로 돌아가게

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
