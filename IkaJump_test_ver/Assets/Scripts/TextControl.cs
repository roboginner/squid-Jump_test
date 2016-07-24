using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TextControl : MonoBehaviour {
    Text info;

    public int hiScore = 012345;
    public int score = 012345;
    public int stage = 01;
    public int time = 134;

    string timeChar = "Ｍ：Ｓ：ＭＳ"; //「分：秒：1/60秒」に変換された文字列
    string[] narrow = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }; // 半角の数字の配列
    string[] wide = { "０", "１", "２", "３", "４", "５", "６", "７", "８", "９" }; // 全角の数字の配列


    void Awake ()
    {
        info = GetComponent<Text>();
        info.text = "ＨＩ　ＳＣＯＲＥ\n" + ReplaceNtoW(hiScore) + "\n \nＳＣＯＲＥ\n" + ReplaceNtoW(score) + "\n \nＳＴＡＧＥ　" + ReplaceNtoW(stage) + "\n \n \n" + timeChar;
	}
	
	void Update ()
    {
	    
	}

    string ReplaceNtoW(int narrowNumber)
    {
        string narrowText = Convert.ToString(narrowNumber);
        string wideText = "";
        int i;
        for(i = 0; i <= 9; i++)
        {
            narrowText = narrowText.Replace(narrow[i], wide[i]);
            Debug.Log(i);
        }
        wideText = narrowText;
        return wideText;
    }
}
