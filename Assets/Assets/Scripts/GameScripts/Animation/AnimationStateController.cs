using UnityEngine;

//[RequireComponent(typeof(Enemy))]
public class AnimationStateController : MonoBehaviour
{
    private Animator animator;

    //ublic Enemy enemy;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey("w"))//enemy.health != 0)
            animator.SetBool("isDead", false);
        else animator.SetBool("isDead", true);
    }
}