using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float lifeTime = 3f;
    void Start()
    {
        Destroy(gameObject, lifeTime); // Auto-destroy after time
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed *Time.deltaTime);
    }
     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { 
            Destroy (other.gameObject);
            Destroy(gameObject);
            Debug.Log("Bullet hit: " + other.name);

        }
        else if (other.TryGetComponent<ToughEnemy>(out ToughEnemy toughenemy))
        {

            toughenemy.OnBulletHit();
            Destroy(gameObject);
        }
        

    }


  
}
