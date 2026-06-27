using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    private Camera mainCamera;
    private Animator anim;
    private NavMeshAgent agent;

    private void Awake()
    {
        mainCamera = Camera.main;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (anim != null) anim.SetFloat("Speed", agent.velocity.magnitude);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                MoveToTouchPosition(touch.position);
            }
        }

        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            MoveToTouchPosition(Input.mousePosition);
        }
        #endif
    }

    private void MoveToTouchPosition(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            if (agent != null) agent.SetDestination(hit.point);
        }
    }
}