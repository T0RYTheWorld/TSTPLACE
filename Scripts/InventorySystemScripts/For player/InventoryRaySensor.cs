using UnityEngine;

[DefaultExecutionOrder(-100)]
public class InventoryRaySensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [Header("RayCast Settings")]
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask rayMask = ~0;
    [Header("AtTheEnd-Sphere Settings")]
    [SerializeField] private float sphereRadius = 0.2f;
    [SerializeField] private LayerMask sphereMask = ~0;
     public Camera PlayerCamera => playerCamera;
    public float SphereRadius => sphereRadius;

    public bool IsReady { get; private set; }
    public bool HasHit { get; private set; }
    public RaycastHit HitInfo { get; private set; }
    public Ray Ray { get; private set; }


    public Vector3 SphereCenter { get; private set; }


    public GameObject TargetObject { get; private set; }
    public Collider TargetCollider { get; private set; }

    private void Awake()
    {
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Refresh();
    }
    public void Refresh()
    {
        ResetResults();

        if (playerCamera == null)
            return;

        IsReady = true;

        UpdateRay();
        FindSphereTarget();
    }
    private void UpdateRay()
    {
        Ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        HasHit = Physics.Raycast(
            Ray,
            out RaycastHit hit,
            rayDistance,
            rayMask,
            QueryTriggerInteraction.Ignore
        );

        HitInfo = HasHit ? hit : default;
        SphereCenter = HasHit
            ? hit.point
            : Ray.GetPoint(rayDistance);
    }
    private void FindSphereTarget()
    {
        Collider[] candidates = Physics.OverlapSphere(
            SphereCenter,
            sphereRadius,
            sphereMask,
            QueryTriggerInteraction.Ignore
        );

        bool bestIsItem = false;
        bool bestIsDirectHit = false;
        float bestDistance = float.PositiveInfinity;

        GameObject directTarget = HasHit
            ? GetTargetObject(HitInfo.collider)
            : null;

        foreach (Collider candidate in candidates)
        {
            GameObject target = GetTargetObject(candidate);

            bool isItem = IsItem(target);
            bool isDirectHit = target == directTarget;

            Vector3 closestPoint =
                candidate.ClosestPoint(SphereCenter);

            float distance =
                (closestPoint - SphereCenter).sqrMagnitude;

            if (TargetObject != null)
            {
                if (isItem != bestIsItem)
                {
                    if (!isItem)
                        continue;
                }
                else if (isDirectHit != bestIsDirectHit)
                {
                    if (!isDirectHit)
                        continue;
                }
                else if (distance >= bestDistance)
                {
                    continue;
                }
            }

            TargetObject = target;
            TargetCollider = candidate;

            bestIsItem = isItem;
            bestIsDirectHit = isDirectHit;
            bestDistance = distance;
        }

        if (TargetObject == null && HasHit)
        {
            TargetCollider = HitInfo.collider;
            TargetObject = directTarget;
        }
    }
    private static GameObject GetTargetObject(Collider source)
    {
        if (source == null)
            return null;

        Transform current = source.transform;

        while (current != null)
        {
            if (IsItem(current.gameObject))
                return current.gameObject;

            current = current.parent;
        }
        
        return source.gameObject;
    }
    private static bool IsItem(GameObject target)
    {
        if (target == null)
            return false;

        string objectTag = target.tag;

        return objectTag == "SceneItem" ||
               objectTag == "WorldItem" ||
               objectTag == "InventoryItem";
    }
    private void ResetResults()
    {
        IsReady = false;
        HasHit = false;

        Ray = default;
        HitInfo = default;
        SphereCenter = Vector3.zero;

        TargetObject = null;
        TargetCollider = null;
    }

    private void OnDisable()
    {
        ResetResults();
    }
}
