using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 5f;
    public float minZoom = 8f;
    public float maxZoom = 16f;
    public float zoomLimiter = 10f;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Camera cam;
    private List<Transform> players = new List<Transform>();

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (players.Count == 0) return;

        MoveCamera();
        ZoomCamera();
    }

    public void AddPlayer(Transform player)
    {
        if(!players.Contains(player))
        {
            players.Add(player);
        }
    }

    public void MoveCamera()
    {
        Vector3 center = GetCenterPoint();

        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float clampedX = Mathf.Clamp(center.x, minBounds.x + camWidth, maxBounds.x - camWidth);
        float clampedY = Mathf.Clamp(center.y, minBounds.y + camHeight, maxBounds.y - camHeight);

        Vector3 newPos = new Vector3(clampedX, clampedY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime);
    }

    void ZoomCamera()
    {
        float greatestDist = GetGreatestDistance();

        float newZoom = Mathf.Lerp(maxZoom, minZoom, greatestDist / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        if(players.Count == 1)
        {
            return players[0].position;
        }

        Bounds bounds = new Bounds(players[0].position, Vector3.zero);

        for(int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        return bounds.center;
    }

    float GetGreatestDistance()
    {
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);

        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        return Mathf.Max(bounds.size.x, bounds.size.y);
    }
}
