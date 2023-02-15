using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Currency : MonoBehaviour
{
    public int count;
    public int currencyGain;
    public TMP_Text currencyAmount;
    // Start is called before the first frame update
    void Start()
    {
        count = PlayerPrefs.GetInt("amount");
    }

    // Update is called once per frame
    void Update()
    {
        currencyAmount.SetText("Currency: " + count);
    }
}
