using UnityEngine;

public class JugadorController : MonoBehaviour
{
    private Vector2Int currentGridPosition;
    private Vector3 targetWorldPosition;
    public float movementSpeed = 8f;
    public bool isMoving = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private static readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    private static readonly int DirectionHash = Animator.StringToHash("Direction");

    [Header("Sprites de Reposo")]
    public Sprite idleForwardSprite;
    public Sprite idleBackSprite;
    public Sprite idleSideSprite;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (animator != null)
        {
            animator.SetBool(IsWalkingHash, false);
            animator.SetInteger(DirectionHash, 0);
        }
        SetIdlePose();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TogglePause();
            }
        }

        if (isMoving)
        {
            AnimateMovement();
        }
        else
        {
            if (GameManager.Instance != null && !GameManager.Instance.IsGamePaused())
            {
                HandleInventoryInput();

                if (!isMoving)
                {
                    HandleInput();
                }
            }
        }
    }

    void AnimateMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWorldPosition) < 0.001f)
        {
            transform.position = targetWorldPosition;
            isMoving = false;
            if (animator != null) { animator.SetBool(IsWalkingHash, false); }
            SetIdlePose();
        }
        else
        {
            if (animator != null)
            {
                if (!animator.enabled) animator.enabled = true;
                animator.SetBool(IsWalkingHash, true);
            }
        }
    }

    void SetIdlePose()
    {
        if (spriteRenderer == null || animator == null) return;

        int currentDirection = animator.GetInteger(DirectionHash);
        Sprite targetSprite = idleForwardSprite;

        switch (currentDirection)
        {
            case 0: targetSprite = idleForwardSprite; spriteRenderer.flipX = false; break;
            case 1: targetSprite = idleBackSprite; spriteRenderer.flipX = false; break;
            case 2: targetSprite = idleSideSprite; break;
        }

        if (targetSprite != null) { spriteRenderer.sprite = targetSprite; }
        else if (idleForwardSprite != null) { spriteRenderer.sprite = idleForwardSprite; }

        if (animator.enabled) { animator.enabled = false; }
    }

    void HandleInventoryInput()
    {
        if (GameManager.Instance == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.UseShield();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.UseCharger();
        }
    }


    void HandleInput()
    {
        if (isMoving) return;
        if (GameManager.Instance == null) { Debug.LogError("HandleInput: ¡Falta GameManager.Instance!"); return; }
        if (animator == null) { Debug.LogError("HandleInput: ¡Falta Animator!"); return; }

        Vector2Int newGridPosition = currentGridPosition;
        bool moveAttempted = false;
        int newDirection = animator.GetInteger(DirectionHash);

        if (Input.GetKeyDown(KeyCode.W)) { newGridPosition += Vector2Int.up; moveAttempted = true; newDirection = 1; if (spriteRenderer != null) spriteRenderer.flipX = false; }
        else if (Input.GetKeyDown(KeyCode.S)) { newGridPosition += Vector2Int.down; moveAttempted = true; newDirection = 0; if (spriteRenderer != null) spriteRenderer.flipX = false; }
        else if (Input.GetKeyDown(KeyCode.A)) { newGridPosition += Vector2Int.left; moveAttempted = true; newDirection = 2; if (spriteRenderer != null) spriteRenderer.flipX = true; }
        else if (Input.GetKeyDown(KeyCode.D)) { newGridPosition += Vector2Int.right; moveAttempted = true; newDirection = 2; if (spriteRenderer != null) spriteRenderer.flipX = false; }

        if (!moveAttempted) { return; }

        if (!IsPositionValid(newGridPosition))
        {
            Debug.Log("¡Chocaste con el borde! -1 Energía");
            GameManager.Instance.LoseEnergy(1);
            return;
        }

        GameManager.TileType nextTile = GameManager.Instance.GetTileType(newGridPosition);

        if (nextTile == GameManager.TileType.Wall)
        {
            Debug.Log("¡Chocaste con un muro! -1 Energía");
            GameManager.Instance.LoseEnergy(1);
        }
        else
        {
            currentGridPosition = newGridPosition;

            if (animator != null)
            {
                if (!animator.enabled) animator.enabled = true;
                animator.SetInteger(DirectionHash, newDirection);
            }

            targetWorldPosition = new Vector3(currentGridPosition.x, currentGridPosition.y, -1f);
            isMoving = true;

            GameManager.Instance.UpdatePlayerPosition(currentGridPosition);
            GameManager.Instance.HandleTileEffect(nextTile, currentGridPosition);
        }
    }

    bool IsPositionValid(Vector2Int position)
    {
        int size = GameManager.BOARD_SIZE;
        return position.x >= 0 && position.x < size &&
               position.y >= 0 && position.y < size;
    }

    public void SetInitialPosition(Vector2Int startPos)
    {
        currentGridPosition = startPos;
        Vector3 startWorldPos = new Vector3(startPos.x, startPos.y, -1f);
        transform.position = startWorldPos;
        targetWorldPosition = startWorldPos;
        isMoving = false;

        if (animator != null && !animator.enabled) { animator.enabled = true; }
        if (animator != null) animator.SetInteger(DirectionHash, 0);

        SetIdlePose();
    }

    public bool IsGamePaused()
    {
        if (GameManager.Instance != null)
        {
            return GameManager.Instance.IsGamePaused();
        }
        return false;
    }
}