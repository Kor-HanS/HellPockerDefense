using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{

    // 색깔: yellow / red / blue / green
    public Color CardColor{get;  set;}
    // 카드 패 : 7 / 8 / 9 / 10 / K / Q / J / A
    public string CardChar { get; set; }

    private int colorRandNum;
    private int cardCharRandNum;

    // 카드 패 및 색깔
    public static Color[] colors = {UnityEngine.Color.red, UnityEngine.Color.blue, UnityEngine.Color.green, UnityEngine.Color.yellow };
    public static string[] cardChars = { "7", "8", "9", "10", "K", "Q", "J", "A" };

    public Card()
    {
        colorRandNum = Random.Range(0, 4);
        cardCharRandNum = Random.Range(0, 8);

        this.CardColor = colors[colorRandNum];
        this.CardChar = cardChars[cardCharRandNum];

    }

    public void newCard()
    {
        colorRandNum = Random.Range(0, 3 + 1);
        cardCharRandNum = Random.Range(0, 8 + 1);

        this.CardColor = colors[colorRandNum];
        this.CardChar = cardChars[cardCharRandNum];
    }
}
