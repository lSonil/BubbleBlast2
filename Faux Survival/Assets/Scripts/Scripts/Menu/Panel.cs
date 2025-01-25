using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject panelToClose;

    public void OpenPanel()
    {
        panelToOpen.SetActive(true);
    }

    public void ClosePanel()
    {
        panelToClose.SetActive(false);
    }
}
