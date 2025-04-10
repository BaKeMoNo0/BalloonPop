using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    private InputManager inputManager;
    public Rigidbody rb;
    private MeshRenderer meshRenderer;
    private new Collider collider;


    private const float SideForce = 15f;
    private const float LiftForce = 6f;

    private const float DeflateSpeed = 1f;
    private const float InflateSpeed = 0.4f;
    private const float MinScale = 1.8f;
    private const float MaxScale = 6f;
    private bool hasExploded = false;
    
    private const float tiltAngle = -40f;
    private const float rotationLerpSpeed = 3f;
    
    private float verticalBoostSpeed = 0f;
    private const float MaxSpeed = 20f;
    private const float BoostAcceleration = 20f;
    private const float BoostDecay = 30f;
    private bool isInPipe = false;
    private const float StuckThreshold = 0.3f;
    
    public GameObject explosionEffect;

    private void Start()
    {
        collider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        if (!rb) rb = GetComponent<Rigidbody>();
        inputManager = FindObjectOfType<InputManager>();
        mainCamera = Camera.main;
        explosionEffect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
    
    void FixedUpdate()
    {
        if (hasExploded) return;

        rb.AddForce(Vector3.up * LiftForce, ForceMode.Force);
        HandleMovement();
        if (rb.velocity.y > MaxSpeed) rb.velocity = new Vector3(rb.velocity.x, MaxSpeed, rb.velocity.z);
        StartCoroutine(ExplodeIfStuck(3f));
    }
    
    private void Update()
    {
        if (hasExploded) return;
        HandleScaling();
    }
    
    private void HandleMovement()
    {
        if (!isInPipe && inputManager && inputManager.isTouching && inputManager.currentTouchPosition.HasValue) 
        {
            Vector2 balloonScreenPos = mainCamera.WorldToScreenPoint(transform.position);
            Vector2 touchPos = inputManager.currentTouchPosition.Value;
            float horizontalDistance = Mathf.Abs(balloonScreenPos.x - touchPos.x);

            if (horizontalDistance <= 50f && transform.localScale.x > MinScale) {
                verticalBoostSpeed += BoostAcceleration * Time.fixedDeltaTime;
                verticalBoostSpeed = Mathf.Clamp(verticalBoostSpeed, 0f, MaxSpeed);
                rb.AddForce(Vector3.up * verticalBoostSpeed, ForceMode.Force);
            } else {
                verticalBoostSpeed = Mathf.MoveTowards(verticalBoostSpeed, 0f, BoostDecay * Time.fixedDeltaTime);
                float direction = balloonScreenPos.x - touchPos.x;
                float horizontalDir = Mathf.Clamp(direction, -1f, 1f);
                Vector3 forceDir = new Vector3(horizontalDir, 0f, 0f);
                rb.AddForce(forceDir * SideForce, ForceMode.Force);
                
                HandleRotation(horizontalDir);
            }
        } else {
            HandleRotation(0f);
        }
    }
    
    void HandleRotation(float horizontalDir)
    {
        float targetZRotation = horizontalDir * tiltAngle;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZRotation);
        Quaternion smoothed = Quaternion.Lerp(rb.rotation, targetRotation, rotationLerpSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(smoothed);
    }
    
    void HandleScaling()
    {
        Vector3 scaleChange = Vector3.zero;

        if (inputManager && inputManager.isTouching && inputManager.currentTouchPosition.HasValue) {
            scaleChange = -Vector3.one * (DeflateSpeed * Time.deltaTime);
        } else if (!isInPipe) {
            scaleChange = Vector3.one * (InflateSpeed * Time.deltaTime);
        }

        Vector3 newScale = transform.localScale + scaleChange;
        float clamped = Mathf.Clamp(newScale.x, MinScale, MaxScale);
        transform.localScale = Vector3.one * clamped;

        if (clamped >= MaxScale) Explode();
    }
    
    public void ApplyExternalForce(Vector3 direction, float forceStrength)
    {
        rb.AddForce(direction.normalized * forceStrength, ForceMode.Impulse);
        float horizontalDir = -Mathf.Sign(direction.x);
        HandleRotation(horizontalDir);
    }
    
    
    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        if (explosionEffect) Instantiate(explosionEffect, transform.position, Quaternion.identity);
        
        meshRenderer.enabled = false;
        collider.enabled = false;
        rb.isKinematic = true;

        StartCoroutine(ReloadSceneAfterDelay(3f));
    }
    
    
    private System.Collections.IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private System.Collections.IEnumerator ExplodeIfStuck(float delay)
    {
        Vector3 lastPosition = transform.position;
        float stuckTime = 0f;

        while (stuckTime < delay) {
            yield return new WaitForSeconds(0.1f);

            float distance = Mathf.Abs(transform.position.y - lastPosition.y);
        
            if (distance > StuckThreshold) {
                stuckTime = 0f;
                lastPosition = transform.position;
            } else {
                stuckTime += 0.1f;
            }
        }
        Explode();
    }
    
    public void ApplyBoostUp(float boostSpeed) { rb.AddForce(Vector3.up * boostSpeed, ForceMode.Impulse);} 
    public void ApplyBoostDown(float boostSpeed) { rb.AddForce(Vector3.down * boostSpeed, ForceMode.Impulse);} 
    public void SetIsInPipe(bool isEntered) { isInPipe = isEntered; }
}