using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  public IEnumerator Shake (float Duration , float Magintude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < Duration) 
        { 
         float x = Random.Range(-1f,1f)* Magintude;
         float y = Random.Range(-1f, 1f) * Magintude;
         transform.localPosition = new Vector3(x, y, originalPos.z);

         elapsed += Time.deltaTime;
         yield return null;
        }

        transform.localPosition = originalPos;


    }
}
