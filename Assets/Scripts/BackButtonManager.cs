using UnityEngine;
using UnityEngine.UI;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] Button _tryapplicationExitButton; //���ø����̼� ���� �õ� ��ư(�ش� ��ư�� ������ ��¥ �����Ұ����� Ȯ�� UI ����)
    [SerializeField] Button _applicationExitButton; //���ø����̼� ���� "Ȯ��" ��ư
    [SerializeField] Button _applicationExitCansleButton; //���ø����̼� ���� "���" ��ư
    [SerializeField] Button _modeSelectExitButton; //������Ʈ ������ ��� ����â���� �������� ������ ��ư
    [SerializeField] Button _moveModeExitButton; //MoveMode���� ��� ����â���� ������ ��ư
    [SerializeField] Button _rotateModeExitButton; //rotateMode���� ��� ����â���� ������ ��ư
    [SerializeField] Button _scaleModeExitButton; //scaleMode���� ��� ����â���� ������ ��ư
    [SerializeField] GameObject _tryExitCanvas; //���ø����̼� ���Ḧ �õ��ϸ� ������ Ȯ�� Canvas
    [SerializeField] GameObject _modeSelectCanvas; //��� ����â ĵ����
    [SerializeField] GameObject _moveModeCanvas;//MoveMode ĵ����
    [SerializeField] GameObject _rotateModeCanvas;//RotateMode ĵ����
    [SerializeField] GameObject _scaleModeCanvas;//ScaleMode ĵ����
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
