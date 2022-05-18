using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField]
    private GameObject _beamEffect;

    [SerializeField]
    private AudioClip _beamAudio;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(_beamEffect, other.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_beamAudio, transform.position);
            Destroy(other.gameObject,0.2f);
        }
    }
}
