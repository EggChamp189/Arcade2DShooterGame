using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject soundEffectPrefab;

    [SerializeField]
    SoundData[] soundEffects;

    public void PlaySound(string sound) 
    {
        // go through the given sound effects aqnd find the specific one needed.
        foreach (SoundData soundEffect in soundEffects) {
            if (soundEffect.name == sound) 
            {
                // make the sound appear at the player once the correct sound is found
                GameObject soundObject = Instantiate(soundEffectPrefab, FindFirstObjectByType<PlayerScript>().transform.position, Quaternion.identity);
                AudioSource audio = soundObject.GetComponent<AudioSource>();
                // if you add an audio slider in the future, then change this code here
                audio.volume = soundEffect.recVolume;
                audio.clip = soundEffect.sound;
                audio.Play();
                float audioLength = audio.clip.length;
                Destroy(soundObject, audioLength);
                // once found and the audio is played, end the for loop early
                break;
            }
        }
    }
}


[System.Serializable]
public class SoundData 
{
    public string name;
    public AudioClip sound;
    public float recVolume;
}