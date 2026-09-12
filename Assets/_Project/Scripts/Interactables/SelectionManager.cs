using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_Info_Text;
    private Camera mainCamera;
    [field: SerializeField] private float maxDistance = 5f;

    void Start()
    {
        mainCamera = Camera.main;
        interaction_Info_Text = interaction_Info_UI.
        GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        //The Grid in Unity is 1 by 1, so x = 0.5 and y = 0.5 is basically the middle of the screen. The raycast is cast from the center of the screen, which is where the crosshair is looking.
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance))
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
            if (selectedTransform.TryGetComponent(out Interactable_Object interactableObject))
            {
                interaction_Info_Text.text = interactableObject.ItemName;
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                interaction_Info_UI.SetActive(false);
            }

            // -> Much more optimized.
        }
        else
        //Without this else case, when we look up into the sky for example, the interaction_Info_UI will still be active, and itll just be floating in the middle of the screen, which is not what we want. So we need to set it to inactive when we are not looking at an interactable object.
        {
            interaction_Info_UI.SetActive(false);
        }
    }
}