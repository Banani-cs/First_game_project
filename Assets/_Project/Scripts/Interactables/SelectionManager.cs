using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject _interactionInfoUI;
    private TextMeshProUGUI _interactionInfoText;
    private Camera _mainCamera;
    [SerializeField] private float _maxDistance = 5f;

    private void Start()
    {
        _mainCamera = Camera.main;
        _interactionInfoText = _interactionInfoUI.
        GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        //The Grid in Unity is 1 by 1, so x = 0.5 and y = 0.5 is basically the middle of the screen. The raycast is cast from the center of the screen, which is where the crosshair is looking.
        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxDistance))
        {
            var selectedTransform = hitInfo.transform;
            /*if (selectedTransform.GetComponent<Interactable_Object>())
            {
                interaction_Info_Text.text = selectedTransform.GetComponent<Interactable_Object>().GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                interaction_Info_UI.SetActive(false);
            }
            This is notoriously unoptimized, because GetComponent is a very expensive function on the CPU
            */
            // We use this instead
            if (selectedTransform.TryGetComponent(out InteractableObject interactableObject))
            {
                _interactionInfoText.text = interactableObject.ItemName;
                _interactionInfoUI.SetActive(true);
            }
            else
            {
                _interactionInfoUI.SetActive(false);
            }

            // -> Much more optimized.
        }
        else
        //Without this else case, when we look up into the sky for example, the interaction_Info_UI will still be active, and itll just be floating in the middle of the screen, which is not what we want. So we need to set it to inactive when we are not looking at an interactable object.
        {
            _interactionInfoUI.SetActive(false);
        }
    }
}