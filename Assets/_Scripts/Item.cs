using UnityEngine;

public class Item : MonoBehaviour
{
    public Vector3 itemPosition;
    public float scaleSpeed = 0.1f;
    public float maxScale = 1f;

    [Header("Z Destroy Settings")]
    public float destroyZ = 50f;

    private bool hasHit = false;
    private float targetX;
    private float currentScale = 0f;
    private Transform target;

    private void Awake()
    {
        SetRandomPointX();
    }

    private void Update()
    {
        float perspective = CameraComponent.focalLength / (CameraComponent.focalLength + itemPosition.z);

        currentScale = Mathf.Lerp(currentScale, maxScale * perspective, Time.deltaTime * scaleSpeed);
        transform.localScale = Vector3.one * currentScale;

        itemPosition.y -= 0.01f;
        itemPosition.x = Mathf.Lerp(itemPosition.x, targetX, Time.deltaTime * 2f);
        itemPosition.z += 0.05f;

        Vector2 screenPos = new Vector2(itemPosition.x, itemPosition.y) * perspective;
        transform.position = new Vector3(screenPos.x, screenPos.y, 0);

        CheckForHit();

        if (itemPosition.z > destroyZ)
        {
            Destroy(gameObject);
        }
    }

    private void SetRandomPointX()
    {
        if (PointsManager.Instance == null) return;

        int rand = Random.Range(0, 3);
        Transform selectedPoint = null;

        switch (rand)
        {
            case 0: selectedPoint = PointsManager.Instance.leftPoint; break;
            case 1: selectedPoint = PointsManager.Instance.middlePoint; break;
            case 2: selectedPoint = PointsManager.Instance.rightPoint; break;
        }

        if (selectedPoint != null)
            targetX = selectedPoint.position.x;
    }

    private void CheckForHit()
    {
        if (hasHit) return;

        target = GameObject.FindWithTag("Player").transform;
        var distance = Mathf.Sqrt(Mathf.Pow(target.position.x - transform.position.x, 2) + Mathf.Pow(target.position.y - transform.position.y, 2));

        if (distance < 1f && !GameManager.Instance.isJumping)
        {
            GameManager.Instance.DamageHealth(5);
            hasHit = true;
        }
    }
}
