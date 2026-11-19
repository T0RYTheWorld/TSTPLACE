using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryInputActions : MonoBehaviour
{
    [SerializeField] private InputActionReference useRightHandAction;
    [SerializeField] private InputActionReference useLeftHandAction;
    [SerializeField] private InputActionReference dropRightHandAction;
    [SerializeField] private InputActionReference dropLeftHandAction;
    [SerializeField] private InputActionReference restoreRightHandAction;
    [SerializeField] private InputActionReference restoreLeftHandAction;
    [SerializeField] private InputActionReference openInventoryAction;
    [SerializeField] private InputActionReference VisualisatingCasts;

    public bool RightHandUsed => WasPressed(useRightHandAction);
    public bool LeftHandUsed => WasPressed(useLeftHandAction);
    public bool RightHandDropped => WasPressed(dropRightHandAction);
    public bool LeftHandDropped => WasPressed(dropLeftHandAction);
    public bool RightHandRestore => WasPressed(restoreRightHandAction);
    public bool LeftHandRestore => WasPressed(restoreLeftHandAction);
    public bool InventoryIsOpen => IsPressed(openInventoryAction);
    public bool CastsAreVisible { get; private set; }
    private void Update()
    {
        if (WasPressed(VisualisatingCasts))
        {
            CastsAreVisible = !CastsAreVisible;
        }
    }

    private void OnEnable()
    {
        EnableAction(useRightHandAction);
        EnableAction(useLeftHandAction);
        EnableAction(dropRightHandAction);
        EnableAction(dropLeftHandAction);
        EnableAction(restoreRightHandAction);
        EnableAction(restoreLeftHandAction);
        EnableAction(openInventoryAction);
        EnableAction(VisualisatingCasts);
    }

    private void OnDisable()
    {
        DisableAction(useRightHandAction);
        DisableAction(useLeftHandAction);
        DisableAction(dropRightHandAction);
        DisableAction(dropLeftHandAction);
        DisableAction(restoreRightHandAction);
        DisableAction(restoreLeftHandAction);
        DisableAction(openInventoryAction);
        DisableAction(VisualisatingCasts);
    }

    private Vector2 ReadVector2(InputActionReference actionReference)
    {
        return actionReference != null ? actionReference.action.ReadValue<Vector2>() : Vector2.zero;
    }

    private bool WasPressed(InputActionReference actionReference)
    {
        return actionReference != null && actionReference.action.WasPressedThisFrame();
    }

    private bool IsPressed(InputActionReference actionReference)
    {
        return actionReference != null && actionReference.action.IsPressed();
    }

    private void EnableAction(InputActionReference actionReference)
    {
        actionReference?.action.Enable();
    }

    private void DisableAction(InputActionReference actionReference)
    {
        actionReference?.action.Disable();
    }
}

