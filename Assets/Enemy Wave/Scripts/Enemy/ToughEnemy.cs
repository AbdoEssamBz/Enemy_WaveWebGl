using UnityEngine;
using UnityEngine.AI;

public class ToughEnemy : MonoBehaviour
{
    private bool IsHitOnce = false;
    private Renderer EnemyRenderer;



    private NavMeshAgent agent;
    private Transform Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyRenderer = GetComponent<Renderer>();
       

        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {

            agent.SetDestination(Player.position);
        }
        
    }

    public void OnBulletHit()
    {
        if (!IsHitOnce)
        {
            IsHitOnce = true;

            if (EnemyRenderer != null)
            {
                EnemyRenderer.material.color = Color.red;
                Debug.Log("Hit One");
            }

        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Hit Two");

        }


    }
}
