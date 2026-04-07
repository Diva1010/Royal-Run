using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource bolderSmashAudioSource;
    CinemachineImpulseSource impulseSource;

    float cooldownTimer = 1f;
    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        if(cooldownTimer < collisionCooldown) return;
        FireImpulse();
        CollisionFX(collision);
        cooldownTimer = 0f;
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = Mathf.Min((1f / distance) * shakeModifier, 1f);
        impulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        bolderSmashAudioSource.Play();
    }
}
