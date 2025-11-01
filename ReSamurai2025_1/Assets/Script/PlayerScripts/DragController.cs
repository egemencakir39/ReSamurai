using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.EventSystems; 
public class DragController : MonoBehaviour
{
    [Header("GameObjects")] 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerClonePrefab;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject arrowIndicatorPrefab;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam;

    [Header("Variables")] 
    [SerializeField] private float force = 10f;
    [SerializeField] private float dragLimit = 10f;
    [SerializeField] private int maxAttack = 5;
    [SerializeField] private GameManager gameManager;
    
    private AttackBarControllerUI attackBarController;
    private SoundManager soundManager;

    private GameObject _lastClone;
    private bool _isDragging;
    private Vector2 _dragStartPosition;
    private Camera _camera;
    private Vector2 _mousePosition;
    private bool _isMoving = false;
    private List<GameObject> cloneList = new List<GameObject>();
    private bool canShoot = true;
    //Yön Oku
    private GameObject arrowInstance;
    private Transform arrowHead;
    private Transform dot1;
    private Transform dot2;
    private SpriteRenderer dot1Renderer;
    private SpriteRenderer dot2Renderer;
    private SpriteRenderer arrowRenderer;

    [Inject]
    public void Construct(AttackBarControllerUI attackBarController,SoundManager soundManager)
    {
        this.attackBarController = attackBarController;
        this.soundManager = soundManager;
    }
    private void Start()
    {
        _camera = Camera.main;
        lineRenderer.enabled = false;
        
        attackBarController.InitBar(maxAttack);
        
        arrowInstance = Instantiate(arrowIndicatorPrefab);
        arrowInstance.SetActive(false); 
        
        dot1 = arrowInstance.transform.Find("dot1")?.transform;
        dot2 = arrowInstance.transform.Find("dot2")?.transform;
        arrowHead = arrowInstance.transform.Find("ArrowHead");

        dot1Renderer = dot1.GetComponent<SpriteRenderer>();
        dot2Renderer = dot2.GetComponent<SpriteRenderer>();
        arrowRenderer = arrowHead.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            return;
        if (maxAttack > 0)
        {
            if (Input.GetMouseButtonDown(0) && !_isDragging)
            {
                DragStart();
            }

            if (_isDragging)
            {
                Drag();
            }

            if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                DragEnd();
            }
            
        }
       

        if (_lastClone != null)
        {
            Rigidbody2D rb = _lastClone.GetComponent<Rigidbody2D>();
            if (rb.velocity.magnitude < 1f)
            {
                rb.velocity = Vector2.zero;
            }
        }

    }

    void DragStart()
    {
        if (!canShoot) return;
        
        _isDragging = true;
        lineRenderer.enabled = true;

        if (_lastClone != null)
        {
            _dragStartPosition = _lastClone.transform.position;
        }
        else
        {
            _dragStartPosition = player.transform.position;
        }

        lineRenderer.SetPosition(0, _dragStartPosition);
        
       
        arrowInstance.SetActive(true); 
        arrowInstance.transform.position = _dragStartPosition;
        gameManager.StartTimer();
    }

    void Drag()
    {
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dragVector = _mousePosition - _dragStartPosition;

        if (dragVector.magnitude > dragLimit)
        {
            dragVector = dragVector.normalized * dragLimit;
        }

        lineRenderer.SetPosition(1, _dragStartPosition + dragVector);
        
        Vector2 dir = -dragVector.normalized; 
        arrowInstance.transform.position = _dragStartPosition + dir * 3f; 

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; 
        arrowInstance.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); 
        
        float pullStrength = dragVector.magnitude / dragLimit; 
        
        Color color = Color.Lerp(Color.green, Color.red, pullStrength);
        arrowRenderer.color = color;
        dot1Renderer.color = color;
        dot2Renderer.color = color;  

        float spacing = Mathf.Lerp(0.3f, 1f, pullStrength);
        
        dot1.localPosition = new Vector3(0f, spacing * .2f, 0f);
        dot2.localPosition = new Vector3(0f, -1f, 0f);
        arrowHead.localPosition = new Vector3(0f, spacing * 1.2f, 0f);
    }
    void DragEnd()
    {
        _isDragging = false;
        lineRenderer.enabled = false;
        
        arrowInstance.SetActive(false); 

        GameObject newClone = Instantiate(playerClonePrefab, _dragStartPosition, Quaternion.identity);
        cloneList.Add(newClone);
        
        vcam.Follow = newClone.transform;

        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (_dragStartPosition - _mousePosition).normalized;

        float forceMagnitude = Vector2.Distance(_dragStartPosition, _mousePosition) * force;
        forceMagnitude = Mathf.Clamp(forceMagnitude, 0, dragLimit * force);

        Rigidbody2D rb = newClone.GetComponent<Rigidbody2D>();
        rb.velocity = direction * forceMagnitude;
        _lastClone = newClone;
        maxAttack--;
        attackBarController.UpdateBar(maxAttack);
        
        soundManager.DragSound();
    }
    public void LockShooting()
    {
        canShoot = false;
    }
    public List<GameObject> GetClones() => cloneList;
}

