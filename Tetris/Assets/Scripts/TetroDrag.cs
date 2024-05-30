using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TetroDrag : MonoBehaviour
{
    [SerializeField] private float lerpFactor = 15;

    private Camera mainCamera;
    private Rigidbody rb;
    private float yPosition;

    private bool isDragging = false;
    private bool canRotate = true;

    private List<TagPosition> positions;
    
    private void Awake()
    {
        yPosition = transform.position.y + 1.5f;
        
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<MainCamera>().GetComponent<Camera>();
    }

    private void Start()
    {
        positions = new(FindObjectsOfType<TagPosition>());
    }

    private void OnMouseDown()
    {
        isDragging = true;
        transform.DOMoveY(yPosition, .2f);
        rb.useGravity = false;
    }

    private void OnMouseDrag()
    {
        Plane plane = new Plane(Vector3.up, Vector3.up * yPosition); 
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if (plane.Raycast(ray, out float distance))
        {
            transform.position = Vector3.Lerp(transform.position, ray.GetPoint(distance), Time.deltaTime * lerpFactor);
        }
    }

    private void Update()
    {
        if (!isDragging || !canRotate) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            canRotate = false;
            transform.DORotate(transform.eulerAngles + Vector3.up * 90, .4f).SetEase(Ease.Linear).OnComplete(() => canRotate = true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            canRotate = false;
            transform.DORotate(transform.eulerAngles + Vector3.up * -90, .4f).SetEase(Ease.Linear).OnComplete(() => canRotate = true);
        }
    }
    
    private void OnMouseUp()
    {
        isDragging = false;
        rb.useGravity = true;
        // FindGoToClosest();
    }

    private void FindGoToClosest()
    {
        TagPosition closest = positions[0];
        float minDistance = 10;
        foreach (TagPosition position in positions)
        {
            float distance = Vector3.Distance(transform.position, position.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = position;
            }
        }

        if (minDistance > 2) return;
        
        transform.DOMove(closest.transform.position, .5f).SetEase(Ease.Linear);
    }
}
