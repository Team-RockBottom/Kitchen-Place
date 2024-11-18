using UnityEngine;
using UnityEngine.UI;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] Button _tryapplicationExitButton; //어플리케이션 종료 시도 버튼(해당 버튼을 누르면 진짜 종료할것인지 확인 UI 노출)
    [SerializeField] Button _applicationExitButton; //어플리케이션 종료 "확정" 버튼
    [SerializeField] Button _applicationExitCansleButton; //어플리케이션 종료 "취소" 버튼
    [SerializeField] Button _modeSelectExitButton; //오브젝트 선택후 모드 선택창에서 메인으로 나가는 버튼
    [SerializeField] Button _moveModeExitButton; //MoveMode에서 모드 선택창으로 나가는 버튼
    [SerializeField] Button _rotateModeExitButton; //rotateMode에서 모드 선택창으로 나가는 버튼
    [SerializeField] Button _scaleModeExitButton; //scaleMode에서 모드 선택창으로 나가는 버튼
    [SerializeField] GameObject _tryExitCanvas; //어플리케이션 종료를 시도하면 나오는 확인 Canvas
    [SerializeField] GameObject _modeSelectCanvas; //모드 선택창 캔버스
    [SerializeField] GameObject _moveModeCanvas;//MoveMode 캔버스
    [SerializeField] GameObject _rotateModeCanvas;//RotateMode 캔버스
    [SerializeField] GameObject _scaleModeCanvas;//ScaleMode 캔버스
    void Start()
    {
        _tryapplicationExitButton.onClick.AddListener(OnTryApplicationExitButton);
        _applicationExitButton.onClick.AddListener(OnApplicationExitButton);
        _applicationExitCansleButton.onClick.AddListener(OnCansleApplicationExit);
        _modeSelectExitButton.onClick.AddListener(OnModeSelectExitButton);
        _moveModeExitButton.onClick.AddListener(OnMoveModeExitButton);
        _rotateModeExitButton.onClick.AddListener(OnRotateModeExitButton);
        _scaleModeExitButton.onClick.AddListener(OnScaleModeExitButton);
    }

    private void OnTryApplicationExitButton()
    {
        _tryExitCanvas.SetActive(true);
    }
    private void OnCansleApplicationExit()
    {
        _tryExitCanvas.SetActive(false);
    }
    private void OnApplicationExitButton()
    {
        Application.Quit();
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
