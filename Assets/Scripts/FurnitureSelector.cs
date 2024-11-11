using UnityEngine;

public class FurnitureSelector : MonoBehaviour
{
    public GameObject uiPanel;  // ������, ȸ��, ũ�� ���� UI �г�

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("ARObject"))  // AR ������Ʈ �±� Ȯ��
                {
                    ActivateUIPanel(hit.transform.gameObject);
                }
            }
        }
    }

    void ActivateUIPanel(GameObject selectedObject)
    {
        uiPanel.SetActive(true);
        uiPanel.GetComponent<UIPanelController>().SetTargetObject(selectedObject);
    }
}