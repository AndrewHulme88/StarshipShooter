using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.4f;
    [SerializeField] float powerUpFiringRate = 2f;
    [SerializeField] float powerUpDuration = 10f;
    [SerializeField] Vector3 bulletOffset;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    private bool powerUpActive = false;

    Coroutine firingCoroutine;
    //AudioPlayer audioPlayer;

    private void Awake()
    {
        //audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (useAI)
            {
                GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, 180));
                Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    rb2d.velocity = -transform.up * projectileSpeed;
                }
                Destroy(instance, projectileLifetime);
            }
            else
            {
                GameObject instance = Instantiate(projectilePrefab, transform.position + bulletOffset, Quaternion.identity);
                Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    rb2d.velocity = transform.up * projectileSpeed;
                }
                Destroy(instance, projectileLifetime);
            }
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            //audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }

    }

    public void IncreaseFiringRate()
    {
        if (!powerUpActive)
        {
            StartCoroutine(PowerUp());
        }
    }

    IEnumerator PowerUp()
    {
        powerUpActive = true;
        float storeBaseFiringRate = baseFiringRate;
        baseFiringRate = baseFiringRate / powerUpFiringRate;
        yield return new WaitForSeconds(powerUpDuration);
        powerUpActive = false;
        baseFiringRate = storeBaseFiringRate;
    }
}
