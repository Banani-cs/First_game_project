using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; } // Declaring Singletoninstance
    [SerializeField] private GameObject _interactionInfoUI;
    private TextMeshProUGUI _interactionInfoText;
    private Camera _mainCamera;
    [SerializeField] private float _maxDistance = 5f;

    public bool onTarget { get; private set; } = false;

    private void Start()
    {
        _mainCamera = Camera.main;
        _interactionInfoText = _interactionInfoUI.
        GetComponentInChildren<TextMeshProUGUI>();
    }

    // Singleton pattern is used here to ensure that there is only one instance of the SelectionManager class in the scene. This allows other scripts to easily access the instance of the SelectionManager class without having to find it in the scene.
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        //The Grid in Unity is 1 by 1, so x = 0.5 and y = 0.5 is basically the middle of the screen. The raycast is cast from the center of the screen, which is where the crosshair is looking.
        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, _maxDistance))
        {
            var selectedTransform = hitInfo.transform;

            InteractableObject interactables = selectedTransform.GetComponent<InteractableObject>();
            //if(selectedTransform.GetComponent<InteractableObject>())
            // This is notoriously unoptimized, because GetComponent is a very expensive function on the CPU
            // We use this instead
            if (interactables != null && interactables.playerInRange && hitInfo.distance <= _maxDistance)
            {
                onTarget = true;
                _interactionInfoText.text = interactables.ItemName;
                _interactionInfoUI.SetActive(true);
            }
            else
            {
                onTarget = false;
                _interactionInfoUI.SetActive(false);
            }

            // -> Much more optimized.
        }
        else
        //Without this else case, when we look up into the sky for example, the interaction_Info_UI will still be active, and itll just be floating in the middle of the screen, which is not what we want. So we need to set it to inactive when we are not looking at an interactable object.
        {
            onTarget = false;
            _interactionInfoUI.SetActive(false);
        }
    }
}