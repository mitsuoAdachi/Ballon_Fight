using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Beam : MonoBehaviour
{
    [SerializeField]
    private GameObject _beamEffect1;
    [SerializeField]
    private GameObject _beamEffect2;

    [SerializeField]
    private AudioClip _beamAudio;

    private bool _noise = false;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _noise = true;
            Instantiate(_beamEffect1, other.transform.position, Quaternion.identity);
            Instantiate(_beamEffect2, other.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_beamAudio, transform.position);
            Destroy(other.gameObject,0.2f);
        }
    }
    private void Update()
    {
        if (_noise == true)
        {
            var _noise = GetComponent<CinemachineImpulseSource>();
            _noise.GenerateImpulse();
            StartCoroutine(OffNoiseCoroutine());
        }
    }
    private IEnumerator OffNoiseCoroutine()
    {
        yield return new WaitForSeconds(1);
        _noise = false;
    }

}

