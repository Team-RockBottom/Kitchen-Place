using UnityEngine;
using UnityEngine.UI;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] Button _applicationExitButton; //메인 화면에서 어플리케이션 종료 버튼
    [SerializeField] Button _modeSelectExitButton; //오브젝트 선택후 모드 선택창에서 메인으로 나가는 버튼
    [SerializeField] Button _moveModeExitButton; //MoveMode에서 모드 선택창으로 나가는 버튼
    [SerializeField] Button _rotateModeExitButton; //rotateMode에서 모드 선택창으로 나가는 버튼
    [SerializeField] Button _scaleModeExitButton; //scaleMode에서 모드 선택창으로 나가는 버튼
    [SerializeField] GameObject _modeSelectCanvas; //모드 선택창 캔버스
    [SerializeField] GameObject _moveModeCanvas;//MoveMode 캔버스
    [SerializeField] GameObject _rotateModeCanvas;//RotateMode 캔버스
    [SerializeField] GameObject _scaleModeCanvas;//ScaleMode 캔버스
    void Start()
    {
        _modeSelectExitButton.onClick.AddListener(OnModeSelectExitButton);
        _moveModeExitButton.onClick.AddListener(OnMoveModeExitButton);
        _rotateModeExitButton.onClick.AddListener(OnRotateModeExitButton);
        _scaleModeExitButton.onClick.AddListener(OnScaleModeExitButton);
    }


    private void OnModeSelectExitButton()
    {
        _modeSelectCanvas.SetActive(false);
    }
    private void OnMoveModeExitButton()
    {
        _modeSelectCanvas.SetActive(true);
        _moveModeCanvas.SetActive(false);
    }
    private void OnRotateModeExitButton()
    {
        _modeSelectCanvas.SetActive(true);
        _rotateModeCanvas.SetActive(false);
    }
    private void OnScaleModeExitButton()
    {
        _modeSelectCanvas.SetActive(true);
        _scaleModeCanvas.SetActive(false);
    }
}
