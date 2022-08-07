using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public Text scoreText , livesText;

    public int lives = 3;
    public float respawnTime = 3.0f;
    public float respawnImmuneTime = 3.0f;

    public int score = 0;

    private void FixedUpdate()
    {
        LivesCounter();
    }

    public void AstroidDestroyed(Asteroid asteroid)
    {
        this.explosion.transform.position = asteroid.transform.position;
        this.explosion.Play();

        if(asteroid.size < 2.0f)
        {
            this.score += 3;
        } else if (asteroid.size > 3.0f)
        {
            this.score += 1;
        } else
        {
            this.score += 2;
        }
        PointsScored();
    }

    public void PlayerDied()
    {
        this.explosion.transform.position = this.player.transform.position;
        this.explosion.Play();

        this.lives--;

        if(this.lives <= 0)
        {
            GameOver();
        } else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnImmuneTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), this.respawnTime);
        PointsScored();
    }

    private void PointsScored()
    {
        this.scoreText.text = score.ToString();
    }

    private void LivesCounter()
    {
        this.livesText.text = "x" + lives.ToString();
    }
}
