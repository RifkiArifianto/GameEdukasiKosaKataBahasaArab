using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacksoundManager : MonoBehaviour
{
    public static BacksoundManager Instance; // Singleton instance
    public AudioSource SourceSuaraMusik;     // AudioSource yang digunakan

    private bool isMuted = false;            // Status mute/unmute
    private float previousVolume = 1f;       // Volume sebelumnya untuk toggle mute

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Hancurkan instance baru
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Fungsi untuk mengubah volume
    public void SetVolume(float volume)
    {
        if (SourceSuaraMusik == null) return;

        SourceSuaraMusik.volume = volume;
        previousVolume = volume; // Simpan volume terbaru
    }

    // Fungsi untuk mute atau unmute
    public void ToggleMute()
    {
        if (SourceSuaraMusik == null) return;

        if (isMuted)
        {
            SourceSuaraMusik.volume = previousVolume; // Kembalikan volume sebelumnya
        }
        else
        {
            previousVolume = SourceSuaraMusik.volume; // Simpan volume sebelum mute
            SourceSuaraMusik.volume = 0;             // Mute
        }

        isMuted = !isMuted;
    }

    // Fungsi untuk menghentikan musik
    public void StopBacksound()
    {
        if (SourceSuaraMusik == null) return;

        SourceSuaraMusik.Stop();
    }

    // Fungsi untuk memutar ulang musik
    public void PlayBacksound()
    {
        if (SourceSuaraMusik == null) return;

        if (!SourceSuaraMusik.isPlaying)
        {
            SourceSuaraMusik.Play();
        }
    }
}
