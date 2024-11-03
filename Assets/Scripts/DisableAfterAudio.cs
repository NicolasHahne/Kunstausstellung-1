using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class DisableAfterAudio : MonoBehaviour
{
    public GameObject player;
    public AudioSource audioSource;
	//audioSource = GetComponentInChildren<AudioSource>(); Suche die AudioSource im Kind-Element
	public int moody;
    private int mood;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        mood = player.GetComponent<MoodSystem>().mood;
		    // Überprüfe den Mood des Spielers, um zu entscheiden, ob das GameObject aktiviert werden soll
        if (mood == moody) // Beispielbedingung, anpassen je nach gewünschtem Verhalten
        {
            gameObject.SetActive(true);
            if (audioSource != null)
            {
                // Starte das Audio
                audioSource.Play();
                // Starte die Coroutine, um zu warten, bis das Audio zu Ende ist
                StartCoroutine(DisableAfterAudioEnds());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }
    
    void Update()
    {
		    // Überprüfe den Mood des Spielers, um zu entscheiden, ob das GameObject aktiviert werden soll
        if (mood == moody) // Beispielbedingung, anpassen je nach gewünschtem Verhalten
        {
            gameObject.SetActive(true);
                if (audioSource != null && !audioSource.isPlaying)
            {
                // Starte das Audio
                audioSource.Play();
                // Starte die Coroutine, um zu warten, bis das Audio zu Ende ist
                StartCoroutine(DisableAfterAudioEnds());
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DisableAfterAudioEnds()
    {
        // Warte, bis das Audio zu Ende ist
        yield return new WaitForSeconds(audioSource.clip.length);
        // Deaktiviere das GameObject und alle Kind-Elemente
        gameObject.SetActive(false);
        player.GetComponent<MoodSystem>().mood++;
    }
}