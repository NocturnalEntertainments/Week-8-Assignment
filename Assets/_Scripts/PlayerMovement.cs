using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpHeight = 2f;

    
    private int currentPosition = 1;
    private float jumpDuration = 0.5f;
    private float jumpTimer = 0f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentPosition > 0)
                currentPosition--;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentPosition < 2)
                currentPosition++;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isJumping)
        {
            GameManager.Instance.isJumping = true;
            jumpTimer = 0f;
        }

        MoveToPosition();
        HandleFakeJump();
    }

    void MoveToPosition()
    {
        Transform target = null;

        if (currentPosition == 0)
            target = PointsManager.Instance.leftPoint;
        else if (currentPosition == 1)
            target = PointsManager.Instance.middlePoint;
        else if (currentPosition == 2)
            target = PointsManager.Instance.rightPoint;

        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    void HandleFakeJump()
    {
        if (GameManager.Instance.isJumping)
        {
            jumpTimer += Time.deltaTime;
            float progress = jumpTimer / jumpDuration;
            float yOffset = Mathf.Sin(progress * Mathf.PI) * jumpHeight;

            transform.position = new Vector3(transform.position.x, startPos.y + yOffset, transform.position.z);

            if (progress >= 1f)
            {
                GameManager.Instance.isJumping = false;
                transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
            }
        }
    }
}
