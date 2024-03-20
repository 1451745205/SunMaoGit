using UnityEngine;

public class DrawDiagonalLines : MonoBehaviour
{
    private Vector3 startPoint;
    private Vector3 endPoint;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = GetMouseWorldPosition();
            endPoint = startPoint;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }

        if (Input.GetMouseButton(0))
        {
            endPoint = GetMouseWorldPosition();
            lineRenderer.SetPosition(1, endPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPoint = GetMouseWorldPosition();
            lineRenderer.SetPosition(1, endPoint);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.point;
        }
        return Vector3.zero;
    }
}