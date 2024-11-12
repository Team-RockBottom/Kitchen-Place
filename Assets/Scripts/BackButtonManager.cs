using UnityEngine;
using UnityEngine.UI;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] Button _applicationExitButton; //���� ȭ�鿡�� ���ø����̼� ���� ��ư
    [SerializeField] Button _modeSelectExitButton; //������Ʈ ������ ��� ����â���� �������� ������ ��ư
    [SerializeField] Button _moveModeExitButton; //MoveMode���� ��� ����â���� ������ ��ư
    [SerializeField] Button _rotateModeExitButton; //rotateMode���� ��� ����â���� ������ ��ư
    [SerializeField] Button _scaleModeExitButton; //scaleMode���� ��� ����â���� ������ ��ư
    [SerializeField] GameObject _modeSelectCanvas; //��� ����â ĵ����
    [SerializeField] GameObject _moveModeCanvas;//MoveMode ĵ����
    [SerializeField] GameObject _rotateModeCanvas;//RotateMode ĵ����
    [SerializeField] GameObject _scaleModeCanvas;//ScaleMode ĵ����
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
