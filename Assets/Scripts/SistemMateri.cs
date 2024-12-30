using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemMateri : MonoBehaviour
{
    [System.Serializable]
    public class DataMateri
    {
        public string Materi_Nama;
        public Sprite Materi_Gambar;
        public Sprite Materi_Arab;
        public Sprite Materi_Abjad;
        public AudioClip Materi_Suara;
    }

    public List<DataMateri> _Data;

    [Header("Data Component")]
    public int Data_Materi;
    public Image Gambar_Materi;
    public Image Gambar_Arab;
    public Image Gambar_Abjad;
    public Text Teks_NamaMateri;
    public Text Teks_Nomor;

    public AudioSource SourceSuara;

    private bool isSwitching = false; // Untuk mencegah pengulangan klik terlalu cepat

    // Start is called before the first frame update
    void Start()
    {
        Data_Materi = 0;
        v_SetMateri();
    }

    public void v_Tombol(bool ArahKanan)
    {
        if (isSwitching) return; // Jangan lakukan aksi jika sedang berpindah
        isSwitching = true;

        if (ArahKanan)
        {
            Data_Materi++;

            if (Data_Materi >= _Data.Count)
            {
                Data_Materi = 0;
            }
        }
        else
        {
            Data_Materi--;

            if (Data_Materi < 0)
            {
                Data_Materi = _Data.Count - 1;
            }
        }

        v_SetMateri();
        StartCoroutine(SwitchCooldown()); // Tambahkan delay untuk klik berikutnya
    }

    private IEnumerator SwitchCooldown()
    {
        yield return new WaitForSeconds(0.2f); // Atur delay 200ms
        isSwitching = false;
    }

    public void v_SetMateri()
    {
        // Validasi data agar tidak null atau kosong
        if (_Data == null || _Data.Count == 0)
        {
            Debug.LogWarning("Data materi kosong!");
            return;
        }

        // Update UI
        Gambar_Materi.GetComponent<Animation>().Play("AnimasiMateri");

        Gambar_Materi.sprite = _Data[Data_Materi].Materi_Gambar;
        Gambar_Arab.sprite = _Data[Data_Materi].Materi_Arab;
        Gambar_Abjad.sprite = _Data[Data_Materi].Materi_Abjad;
        Teks_NamaMateri.text = _Data[Data_Materi].Materi_Nama;
        Teks_Nomor.text = (Data_Materi + 1) + " / " + _Data.Count;

        // Update audio clip saja, tanpa memutar
        v_SetSuara();
    }

    public void v_SetSuara()
    {
        if (SourceSuara == null)
        {
            Debug.LogWarning("AudioSource tidak diatur!");
            return;
        }

        if (SourceSuara.isPlaying)
        {
            SourceSuara.Stop();
        }

        SourceSuara.clip = _Data[Data_Materi].Materi_Suara;
    }

    public void v_PanggilSuara()
    {
        if (SourceSuara.clip == null)
        {
            Debug.LogWarning("Tidak ada audio clip untuk diputar!");
            return;
        }

        if (SourceSuara.isPlaying)
        {
            SourceSuara.Stop();
        }

        SourceSuara.Play();
    }
}
