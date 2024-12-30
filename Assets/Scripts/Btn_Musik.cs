using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Musik : MonoBehaviour
{
    public Sprite[] GambarTombol; // Gambar untuk tombol On dan Off
    public Image Tombol;         // Komponen Image untuk tombol
    public Text TeksTombol;      // Komponen Text untuk tombol

    private void OnEnable()
    {
        if (BacksoundManager.Instance == null || BacksoundManager.Instance.SourceSuaraMusik == null)
        {
            return;
        }

        if (BacksoundManager.Instance.SourceSuaraMusik.isPlaying)
        {
            Tombol.sprite = GambarTombol[0]; // Gambar On
            TeksTombol.text = "Musik On";
        }
        else
        {
            Tombol.sprite = GambarTombol[1]; // Gambar Off
            TeksTombol.text = "Musik Off";
        }
    }

    public void v_BtnMusik()
    {
        if (BacksoundManager.Instance == null || BacksoundManager.Instance.SourceSuaraMusik == null)
        {
            return;
        }

        if (BacksoundManager.Instance.SourceSuaraMusik.isPlaying)
        {
            BacksoundManager.Instance.SourceSuaraMusik.Pause();
            Tombol.sprite = GambarTombol[1]; // Gambar Off
            TeksTombol.text = "Musik Off";
        }
        else
        {
            BacksoundManager.Instance.SourceSuaraMusik.UnPause();
            Tombol.sprite = GambarTombol[0]; // Gambar On
            TeksTombol.text = "Musik On";
        }
    }
}