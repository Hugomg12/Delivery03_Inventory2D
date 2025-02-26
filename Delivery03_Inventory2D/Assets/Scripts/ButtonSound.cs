using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource buttonAudio;

    public void PlaySound()
    {
        buttonAudio.Play();
    }
}
