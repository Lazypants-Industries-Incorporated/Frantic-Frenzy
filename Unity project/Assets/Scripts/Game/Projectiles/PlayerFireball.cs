using UnityEngine;

public class PlayerFireball : MonoBehaviour
{

    public float speed;
    public int damage;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    //Avada kedavra, hokus pokus, wingardium leviosa
    public void Dir(Vector3 value)
    {
        //Rotate sprite towards current facing direction
        float angle = Mathf.Atan2(value.y, value.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            switch (collision.tag)
            {
                case "EnemyMelee":
                    Destroy(gameObject);
                    collision.SendMessage("TakeDamage", damage);
                    break;
                case "EnemyRanged":
                    Destroy(gameObject);
                    collision.SendMessage("TakeDamage", damage);
                    break;
				case "Battery":
					Destroy(gameObject);
					collision.SendMessage("TakeDamage", damage);
					break;
                case "Player":
                    break;
                case "SpikeTrap":
                    break;
                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
