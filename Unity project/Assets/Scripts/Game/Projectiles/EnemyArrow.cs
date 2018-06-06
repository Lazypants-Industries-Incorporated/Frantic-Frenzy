using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    public float speed;
    public int damage;
    Vector3 dir;

    void Start()
    {
        dir = Vector2.zero;
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    //Avada kedavra, hokus pokus, wingardium leviosa
    public void Dir(Vector2 moveDirection)
    {
        //Rotate sprite towards current facing direction
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                Destroy(gameObject);
                collision.SendMessage("TakeDamage", 1);
                break;
            case "EnemyMelee":
                break;
            case "EnemyRanged":
                break;
            case "EnemyArrow":
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
}
