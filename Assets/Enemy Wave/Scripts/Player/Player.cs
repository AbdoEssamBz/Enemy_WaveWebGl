using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [Header("BulletSetting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    [SerializeField] float ShootDistance = 5f;
   
    
    
    [SerializeField] LayerMask enemyLayer; //

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 Movement = new Vector3(moveX,0 ,moveZ) * Time.deltaTime;
        
        Vector3 Velocity = rb.linearVelocity;
        Velocity.x = moveX;
        Velocity.z = moveZ;
        rb.linearVelocity = Velocity;
        // Keep gravity by preserving Y velocity
    }

     void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ShootRay();
            //Debug.Log("EnemyHit");

        }
    }


    void ShootRay()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

    }
}
