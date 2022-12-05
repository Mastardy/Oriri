using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EventSystemSelectionInitializer : MonoBehaviour
{
    [SerializeField] private PanelSettings panelSettings;

    private void Start()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(EventSystem.current.transform.Find(panelSettings.name).gameObject);
        }
    }
}
