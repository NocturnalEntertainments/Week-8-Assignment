using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;

    public Transform leftPoint;
    public Transform middlePoint;
    public Transform rightPoint;

    private void Awake()
    {
        Instance = this;
    }
}
