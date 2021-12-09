using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float expReward;
    [SerializeField] private TMP_Text HPtext;
    [SerializeField] private GameObject impactParticles;
    [SerializeField] private GameObject deathParticles;

    public event Action OnEnemyDeath;

    private void Start()
    {
        OnEnemyDeath += PlayDeathParticle;
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            ShipProjectile incomingProjectile = other.GetComponent<ShipProjectile>();
            if (incomingProjectile != null)
            {
                GetDamaged(incomingProjectile.damage);
                PlayImpactParticle(other.transform.position);
            }
        }
    }

    private void GetDamaged(float damage)
    {
        hp -= damage;
        hp = Mathf.Clamp(hp, 0, hp);
        UpdateUI();

        if (hp <= 0)
        {
            hp = 0;
            OnEnemyDeath?.Invoke();
        }
    }

    private void UpdateUI()
    {
        HPtext.text = "HP: "+hp;
    }

    private void PlayImpactParticle(Vector3 position)
    {
        Destroy(Instantiate(impactParticles, position, impactParticles.transform.rotation), 0.3f);
    }
    
    private void PlayDeathParticle()
    {
        Destroy(Instantiate(deathParticles, transform.position, deathParticles.transform.rotation), 0.3f);
        Destroy(gameObject, 0.3f);
        //Reward exp to player
        ProgressionManager.instance.GainEXP(expReward);
    }

    private void OnDestroy()
    {
        OnEnemyDeath -= PlayDeathParticle;
    }
}
