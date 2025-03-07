using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
public class PooledBullet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Animator animator { get; private set; }
    public CircleCollider2D circleCollider2D { get; private set; }

    [field: SerializeField] public Vector3 moveDirection { get; private set; }
    [field: SerializeField] public BulletVisual visual { get; private set; }

    [SerializeField] private float moveSpeed;

    /// <summary>
    /// Despawns based on screen UV (0->1 XY values) expanded by these values in +/- directions.
    /// </summary>
    private readonly static Vector2 despawnBoundary = new Vector2(0.08f, 0.05f);


    /// <summary>
    /// Initialize the bullet with the given properties
    /// </summary>
    /// <param name="initializeFrom">A bullet component from a prefab to copy values from.</param>
    /// <param name="order">Sorting order of the sprite this bullet uses</param>
    /// <param name="position">Starting position</param>
    /// <param name="moveSpeed">The speed the bullet moves at</param>
    /// <param name="angle">The bearing angle the bullet will move in</param>
    /// <param name="timeSkip">How many seconds to update this bullet by initially (Set this value if trying to spawn bullets at time based intervals)</param>
    public void Initialize (
        PooledBullet initializeFrom,
        int order,
        Vector3 position,
        float moveSpeed,
        float angle,
        float timeSkip = 0f
    ) {
        CopyComponentsFromPrefab(initializeFrom);

        transform.position = position;
        spriteRenderer.sortingOrder = order;
        this.moveSpeed = moveSpeed;
        moveDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
        transform.position += moveSpeed * moveDirection * timeSkip;
    }

    /// <summary>
    /// Ensures all required components are assigned. Used for Prefabs, who are not isntantiated, but need these values assigned.
    /// </summary>
    public void AssignComponents() {
        spriteRenderer = spriteRenderer ? spriteRenderer : GetComponent<SpriteRenderer>();
        animator = animator ? animator : GetComponent<Animator>();
        circleCollider2D = circleCollider2D ? circleCollider2D : GetComponent<CircleCollider2D>();
    }


    private void CopyComponentsFromPrefab(PooledBullet initializeFrom) {
        AssignComponents();

        // Copy Data From: transform
        transform.localScale = initializeFrom.transform.localScale;
        transform.localRotation = initializeFrom.transform.localRotation;

        // Copy Data From: sprite renderer
        spriteRenderer.enabled = initializeFrom.spriteRenderer.enabled;
        spriteRenderer.sprite = initializeFrom.spriteRenderer.sprite;
        
        // spriteRenderer.material = initializeFrom.spriteRenderer.material;
        spriteRenderer.flipX = initializeFrom.spriteRenderer.flipX;
        spriteRenderer.flipY = initializeFrom.spriteRenderer.flipY;
        spriteRenderer.color = initializeFrom.spriteRenderer.color;

        // Copy Data From: animator
        animator.enabled = initializeFrom.animator.enabled;
        if (animator.enabled) {
            animator.runtimeAnimatorController = initializeFrom.animator.runtimeAnimatorController;
            if (animator.runtimeAnimatorController.animationClips.Length > 0) {
                string stateName = animator.runtimeAnimatorController.animationClips[0].name;
                animator.Play(stateName, 0, 0f);
            } else {
                Debug.LogError($"Bullet \"{name}\" tried to enable an animator from \"{initializeFrom.name}\" with no valid animationClips");
            }
        }

        // Copy Data From: circle collider 2D
        circleCollider2D.enabled = initializeFrom.circleCollider2D.enabled;
        circleCollider2D.radius = initializeFrom.circleCollider2D.radius;
        circleCollider2D.offset = initializeFrom.circleCollider2D.offset;

        /* COPY OTHER DATA BETWEEN PREFAB AND POOLED BULLET HERE! */
        /* ... */
        /* ... */
        /* ... */
    }

    private void Start() {
        AssignComponents();
    }

    private void OnEnable() {
        AssignComponents();
    }

    private void Update() {
        transform.position += moveSpeed * moveDirection * Time.deltaTime;

        Vector2 bulletUV = Camera.main.WorldToViewportPoint(transform.position);
        if (bulletUV.x < -despawnBoundary.x  || bulletUV.x > 1f + despawnBoundary.x || bulletUV.y < -despawnBoundary.y || bulletUV.y > 1f + despawnBoundary.y) {
            BulletPool.Removebullet(this);
        }
    }
}
