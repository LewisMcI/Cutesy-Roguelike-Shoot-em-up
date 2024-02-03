using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<Sound> sounds = new List<Sound>();
    private Dictionary<string, List<AudioSource>> soundDictionary = new Dictionary<string, List<AudioSource>>();

    void Awake()
    {
        // Ensure there is only one instance of the AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Add the sounds to the dictionary for quick access
        foreach (Sound sound in sounds)
        {
            if (!soundDictionary.ContainsKey(sound.name))
            {
                soundDictionary[sound.name] = new List<AudioSource>();
            }

            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;

            soundDictionary[sound.name].Add(source);
        }
    }

    public void PlaySFX(string name)
    {
        if (!soundDictionary.ContainsKey(name) || soundDictionary[name].Count == 0)
        {
            Debug.LogWarning("No sound with name " + name + " found.");
            return;
        }

        List<AudioSource> sources = soundDictionary[name];
        int index = Random.Range(0, sources.Count);
        sources[index].Play();
    }

    public void PlayMusic(string name)
    {
        if (!soundDictionary.ContainsKey(name) || soundDictionary[name].Count == 0)
        {
            Debug.LogWarning("No sound with name " + name + " found.");
            return;
        }

        List<AudioSource> sources = soundDictionary[name];
        int index = Random.Range(0, sources.Count);
        sources[index].Play();
    }

    public void StopMusic(string name)
    {
        if (!soundDictionary.ContainsKey(name) || soundDictionary[name].Count == 0)
        {
            Debug.LogWarning("No sound with name " + name + " found.");
            return;
        }

        foreach (var source in soundDictionary[name])
        {
            source.Stop();
        }
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = .1f;
    [Range(0.1f, 3f)] public float pitch = 1;
    public bool loop;
}
