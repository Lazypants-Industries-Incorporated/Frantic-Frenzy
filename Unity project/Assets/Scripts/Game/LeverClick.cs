using UnityEngine;

public class LeverClick : MonoBehaviour {

    public GameObject player;

    private Animator anim;
    private AudioSource aSource;

	void Start () {
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
	}

    void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1)
        {
            if (!anim.GetBool("LeverClicked"))
                anim.SetBool("LeverClicked", true);
            else
                anim.SetBool("LeverClicked", false);
            aSource.Play();
        }
    }
}
