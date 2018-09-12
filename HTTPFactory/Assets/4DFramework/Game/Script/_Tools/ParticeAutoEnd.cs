using UnityEngine;

public class ParticeAutoEnd : MonoBehaviour
{
    private float particleRealTime;

    void Start()
    {
        particleRealTime = ParticleSystemLength(transform);
    }

    void Update()
    {
        //计时
        particleRealTime -= Time.deltaTime;
        if (particleRealTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private float ParticleSystemLength(Transform transform)
    {
        ParticleSystem[] particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
        float maxDuration = 0;
        foreach (ParticleSystem ps in particleSystems)
        {
            if (ps.emission.enabled)
            {
                float dunration = 0f;
                dunration = ps.startDelay + ps.startLifetime + ps.duration;
                if (dunration > maxDuration)
                {
                    maxDuration = dunration;
                }
            }
        }
        return maxDuration;
    }
}
