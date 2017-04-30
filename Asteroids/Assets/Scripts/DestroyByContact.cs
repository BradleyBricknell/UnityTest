using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;

    public GameObject playerExplosion;

    public int scoreValue;

    private GameController gameController;

    void Start()
    {
        var gameControllerObject = GameObject.FindWithTag("GameController");

        gameController = gameControllerObject.GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other)
    {       
        if (other.tag == "Boundary")
        {
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

            gameController.GameOver();
        }

        gameController.AddScore(scoreValue);

        Destroy(other.gameObject);

        Destroy(gameObject);
    }
}
