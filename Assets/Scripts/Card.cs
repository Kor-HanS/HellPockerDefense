using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{

    // ����: yellow / red / blue / green
    public Color CardColor{get;  set;}
    // ī�� �� : 7 / 8 / 9 / 10 / K / Q / J / A
    public string CardChar { get; set; }

    private int colorRandNum;
    private int cardCharRandNum;

    // ī�� �� �� ����
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
