using UnityEngine;

public class Shooter : MonoBehaviour
{
    //todo: jakis plik do przechowywania kosztow zycia i obrazen poszczegolnych obiektow
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject gun;
    private Animator animator;

    private GameObject projectileParent;
    private const string PROJECTILE_PARENT_NAME = "Projectiles";
    
    private AttackerSpawner myLaneSpawner;
    void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
        CreateProjectileParent();
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
    }

    void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach (var spawner in spawners)
        {
            bool IsCloseEnough = (Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon);

            if (IsCloseEnough)
                myLaneSpawner = spawner;
        }
    }

    private bool IsAttackerInLane()
    {
        if (myLaneSpawner.transform.childCount > 0)
            return true;
        return false;
    }
    
    public void Fire(float damage)
    {
        GameObject newProjectile = Instantiate(projectile, gun.transform.position, Quaternion.identity);

        newProjectile.transform.parent = projectileParent.transform;
    }
}
