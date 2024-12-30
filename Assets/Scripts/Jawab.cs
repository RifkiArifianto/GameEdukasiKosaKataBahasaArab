using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jawab : MonoBehaviour
{
    public TMP_Text skorText;               // Referensi untuk teks skor saat ini
    public GameObject feed_benar, feed_salah; // Feedback benar dan salah
    public GameObject popupSelesai;         // Referensi ke pop-up selesai
    public TMP_Text textScore;              // Teks skor di pop-up
    public TMP_Text textRanking;            // Teks ranking di pop-up
    public GameObject labelScore;           // Referensi ke objek labelscore yang ingin dinonaktifkan
    public float delayFeedback = 1.5f;      // Durasi jeda untuk feedback sebelum berpindah soal
    private bool sudahDijawab = false;      // Flag untuk mencegah klik berulang

    private void Start()
    {
        // Inisialisasi skor jika belum ada
        if (!PlayerPrefs.HasKey("skor"))
        {
            PlayerPrefs.SetInt("skor", 0);
        }

        // Perbarui teks skor di awal
        UpdateSkorText();
        UpdateTextScore();
        popupSelesai.SetActive(false); // Pop-up harus tidak aktif di awal
    }

    public void jawaban(bool jawab)
    {
        if (sudahDijawab) return; // Jika sudah dijawab, hentikan eksekusi

        sudahDijawab = true; // Tandai soal ini sudah dijawab

        if (jawab)
        {
            feed_benar.SetActive(false);
            feed_benar.SetActive(true);

            int skor = PlayerPrefs.GetInt("skor") + 5; // Tambah skor
            PlayerPrefs.SetInt("skor", skor);

            UpdateSkorText();
            UpdateTextScore(); // Pastikan TextScore diperbarui
        }
        else
        {
            feed_salah.SetActive(false);
            feed_salah.SetActive(true);
        }

        // Memulai Coroutine untuk memberikan jeda sebelum berpindah soal
        StartCoroutine(TransitionToNextQuestion());
    }

    IEnumerator TransitionToNextQuestion()
    {
        // Tunggu selama durasi yang ditentukan
        yield return new WaitForSeconds(delayFeedback);

        // Nonaktifkan soal ini
        gameObject.SetActive(false);

        // Pindah ke soal berikutnya jika ada
        int siblingIndex = transform.GetSiblingIndex();
        Transform parent = transform.parent;

        if (siblingIndex + 1 < parent.childCount)
        {
            parent.GetChild(siblingIndex + 1).gameObject.SetActive(true);
        }
        else
        {
            // Jika semua soal selesai, tampilkan pop-up
            ShowPopup();
        }
    }

    void UpdateSkorText()
    {
        if (skorText != null)
        {
            skorText.text = "Skor: " + PlayerPrefs.GetInt("skor", 0).ToString();
        }
    }

    void UpdateTextScore()
    {
        if (textScore != null)
        {
            // Reset textScore di pop-up ke 0
            textScore.text = "0"; // Nilai ini akan ditampilkan saat pop-up muncul
        }
    }

    void ShowPopup()
    {
        popupSelesai.SetActive(true); // Aktifkan pop-up selesai

        // Menonaktifkan labelScore saat pop-up muncul
        if (labelScore != null)
        {
            labelScore.SetActive(false); // Menonaktifkan labelScore di UI
        }

        // Ambil total skor
        int totalSkor = PlayerPrefs.GetInt("skor", 0);

        // Perbarui textScore di pop-up, di sini Anda menggunakan nilai 0 di awal
        UpdateTextScore();

        // Perbarui ranking setelah skor diupdate
        textRanking.text = GetRanking(totalSkor);
    }

    string GetRanking(int skor)
    {
        // Tentukan ranking berdasarkan skor
        if (skor > 80)
        {
            return "LUAR BIASA";
        }
        else if (skor > 60)
        {
            return "CUKUP BAIK";
        }
        else
        {
            return "BELAJAR LAGI";
        }
    }

    public void MainLagi()
    {
        // Reset skor dan muat ulang scene
        PlayerPrefs.SetInt("skor", 0);

        // Pastikan UI diperbarui
        UpdateSkorText();
        UpdateTextScore(); // Reset textScore di UI utama juga

        // Muat ulang scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void KembaliKeMenu()
    {
        // Kembali ke menu utama
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene"); // Ganti "MenuScene" dengan nama scene menu Anda
    }
}
