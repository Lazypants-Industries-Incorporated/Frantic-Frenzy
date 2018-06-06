using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1BossIcicles : MonoBehaviour {

    public float speed;
    public int damage;

    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
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
            case "EnemyClone":
                break;
            case "Balcony":
                break;
            case "Battery":
                break;
            case "Boss":
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

}
