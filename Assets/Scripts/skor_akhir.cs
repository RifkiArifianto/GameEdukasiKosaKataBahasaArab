using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class skor_akhir : MonoBehaviour
{
    public TMP_Text skor_T, rangking_T;  // Referensi untuk teks skor dan ranking

    void Start()
    {
        // Ambil skor dari PlayerPrefs (dari skrip Jawab yang sudah menyimpannya)
        int skor = PlayerPrefs.GetInt("skor", 0);

        // Menampilkan skor di UI
        skor_T.text = skor.ToString();

        // Tentukan ranking berdasarkan skor
        if (skor > 80)
        {
            rangking_T.text = "LUAR BIASA";
        }
        else if (skor > 60)
        {
            rangking_T.text = "CUKUP BAIK";
        }
        else
        {
            rangking_T.text = "BELAJAR LAGI";
        }
    }
}
