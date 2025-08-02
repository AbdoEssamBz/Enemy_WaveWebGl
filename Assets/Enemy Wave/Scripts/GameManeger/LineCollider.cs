using UnityEngine;
using EZCameraShake;
using System.Collections;
using UnityEngine.Experimental.GlobalIllumination;
public class LineCollider : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] Light areaLight;
    [SerializeField] Light DirectionalLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("ToughEnemy"))
        {
            Destroy(other.gameObject);
            CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
            StartCoroutine(FlashLight());
            playerHealth.TakeDamage(1);

          //  StartCoroutine(AreaFlashLight());

        }

    }

    private IEnumerator FlashLight()
    {
        if (areaLight != null && DirectionalLight !=null)
        {
            // DirectionLight Fade Out
            float dirStartIntensity = DirectionalLight.intensity;
            float fadeOutTime = 0.2f;
            float elapsed = 0f;

            while (elapsed < fadeOutTime)
            {
                elapsed += Time.deltaTime;
                DirectionalLight.intensity = Mathf.Lerp(dirStartIntensity, 0f, elapsed / fadeOutTime);
                yield return null;
            }

            DirectionalLight.enabled = false;


            // Area light Fade
            areaLight.enabled = true;
            areaLight.intensity = 0f;

            float explosionPeak = 1500f;
            float riseTime = 0.05f;   // fast burst rise
            float holdTime = 0.08f;    // hold the peak briefly
            float fadeTime = 0.2f;    // fade out duration

            // Flash up to peak
            float riseElapsed = 0f;
            while (riseElapsed < riseTime)
            {
                riseElapsed += Time.deltaTime;
                areaLight.intensity = Mathf.Lerp(0f, explosionPeak, riseElapsed / riseTime);
                yield return null;
            }

            areaLight.intensity = explosionPeak;

            // Hold at peak
            yield return new WaitForSeconds(holdTime);

            // Fade back down
            float fadeElapsed = 0f;
            while (fadeElapsed < fadeTime)
            {
                fadeElapsed += Time.deltaTime;
                areaLight.intensity = Mathf.Lerp(explosionPeak, 0f, fadeElapsed / fadeTime);
                yield return null;
            }

            areaLight.intensity = 0f;
            areaLight.enabled = false;



            // STEP 3: Fade back in the directional light

            DirectionalLight.enabled = true;
            float fadeInTime = 0.2f;
            float fadeInElapsed = 0f;

            while (fadeInElapsed < fadeInTime)
            {
                fadeInElapsed += Time.deltaTime;
                DirectionalLight.intensity = Mathf.Lerp(0f, dirStartIntensity, fadeInElapsed / fadeInTime);
                yield return null;
            }

            DirectionalLight.intensity = dirStartIntensity; // ensure it ends exact
        }
    }



    private IEnumerator AreaFlashLight()
    {
        if (areaLight != null)
        {
            areaLight.enabled = true;
            areaLight.intensity = 1500f; // super bright flash

            float Areaduration = 0.5f; // fade-out duration
            float AreaDuration = 0f;
            float AreastartIntensity = areaLight.intensity;

            while (AreaDuration < Areaduration)
            {
                AreaDuration += Time.deltaTime;
                float newIntensity = Mathf.Lerp(AreastartIntensity, 0f, AreaDuration / Areaduration);
                areaLight.intensity = newIntensity;
                yield return null;
            }

            areaLight.enabled = false;
        }
    }



    private IEnumerator AreaFlashLightFinal()
    {
        if (areaLight != null)
        {
            areaLight.enabled = true;
            areaLight.intensity = 1500f; // super bright flash

            float Areaduration = 0.5f; // fade-out duration
            float AreaDuration = 0f;
            float AreastartIntensity = areaLight.intensity;

            while (AreaDuration < Areaduration)
            {
                AreaDuration += Time.deltaTime;
                float newIntensity = Mathf.Lerp(AreastartIntensity, 0f, AreaDuration / Areaduration);
                areaLight.intensity = newIntensity;
                yield return null;
            }

            areaLight.enabled = false;
        }
    }

}

  
