using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool instance;

    private ObjectPool<AudioSource> _pool;
    
    void Awake()
    {
        instance = this;

        // create a pool with (create, get, release, destroy)
        _pool = new ObjectPool<AudioSource>(
            () =>
            {
                // Create function
                GameObject newSource = new GameObject("AudioSource");
                var s = newSource.AddComponent<AudioSource>();
                s.playOnAwake = false;
                return s;
            }, source =>
            {
                // Get
                source.gameObject.SetActive(true);
            }, source =>
            {
                // Release
                source.gameObject.SetActive(false);
            }, source =>
            {
                // Release but at max capacity -> destroy
                Destroy(source.gameObject);
            }, true, 20, 50);
    }

    private void OnDisable()
    {
        instance = null;
    }

    public AudioSource GetAudioSource()
    {
        return _pool.Get();
    }
    public void ReleaseAudioSource(AudioSource _source)
    {
        _pool.Release(_source);
    }

}
