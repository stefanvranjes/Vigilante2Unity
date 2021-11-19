using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private float _fadeInDuration = 5.0f;

    private float _maxVolume;

    private IEnumerator Start()
    {
        _maxVolume = _audioSource.volume;
        _audioSource.volume = 0.0f;
        float time = 0.0f;
        while (time <= _fadeInDuration)
        {
            float t = time / _fadeInDuration;
            _audioSource.volume = Mathf.Lerp(0.0f, _maxVolume, t);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void SetState(bool isActive)
    {
        _audioSource.mute = !isActive;
    }
}
