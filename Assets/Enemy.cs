using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    private bool dead;

    public event Action OnEnemyDeath;

    private void Start()
    {
        OnEnemyDeath += OnDeath;
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

        if (hp <= 0 && !dead)
        {
            hp = 0;
            dead = true;
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
    
    private void OnDeath()
    {
        HPtext.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetComponent<SphereCollider>().enabled = false;
        Destroy(Instantiate(deathParticles, transform.position, deathParticles.transform.rotation), 0.3f);
        
        //Reward exp to player
        ProgressionManager.instance.GainEXP(expReward);
        this.enabled = false;
    }

    private void OnDestroy()
    {
        OnEnemyDeath -= OnDeath;
    }
}
