using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Skor : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("skor", 0);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("skor").ToString();
    }
}
