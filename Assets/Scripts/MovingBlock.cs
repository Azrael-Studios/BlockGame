using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public Vector3 movementSpeed;
    public float moveDistance;
    // public Vector3 startLocation;
    public GameObject deadMenu;

    Vector3 startLocation;
    public GameManager GameManager;

    // Start is called before the first frame update
    void Awake()
    {

    }
    void Start()
    {
        startLocation = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        deadMenu.SetActive(true);
        GameManager.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        if(ShouldReturn())
        {
            Vector3 moveDirection = movementSpeed.normalized;
            startLocation = startLocation + moveDirection * moveDistance;
            transform.position = startLocation;
            movementSpeed = -movementSpeed;
        }
        else
        {
            Vector3 currentLocation = transform.position;
            currentLocation = currentLocation + movementSpeed * Time.deltaTime;
            transform.position = currentLocation;
        }
    }

    bool ShouldReturn()
    {
        return GetDistanceMoved() > moveDistance;
    }

    float GetDistanceMoved()
    {
        float distanceMoved = Vector3.Distance(startLocation, transform.position);
        return distanceMoved;
    }
}