using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CardHand
{
    Flush, 
    RoyalStraightFlush, 
    BackStraightFlush,
    StraightFlush,
    FiveCardFlush,
    FiveCard, 
    Mountain,
    FourCard, 
    BackStraight,
    Straight, 
    FullHouse, 
    Triple, 
    TwoPair, 
    OnePair, 
    Top 
};

public class PockerGenerater : MonoBehaviour
{


    [SerializeField]
    private TMP_Text[] cardPlaces;
    private Card[] cardArray;
    private int towerCardHand = 0;
    private int[] cardColors; 
    private int[] cardHands; 

    private void Awake()
    {
        cardArray = new Card[5];
    }

    public void NewCards()
    {
        for(int i = 0; i < 5; i++)
        {
            cardArray[i] = new Card();
            cardPlaces[i].SetText(cardArray[i].CardChar);
            cardPlaces[i].color = cardArray[i].CardColor;
        }
    }
    public void ChangeCards(int index)
    {
        cardArray[index] = new Card();
        cardPlaces[index].SetText(cardArray[index].CardChar);
        cardPlaces[index].color = cardArray[index].CardColor;
    }
    public int CheckCardHands()
    {
        cardColors = new int[4];
        cardHands = new int[8];

        for(int i = 0; i < 5; i++)
        {
            Color nowColor = cardArray[i].CardColor;
            string nowCardChar = cardArray[i].CardChar;

            for(int j = 0; j < Card.colors.Length; j++)
            {
                if(Card.colors[j] == nowColor)
                {
                    this.cardColors[j]++;
                    break;
                }
            }
            for(int k = 0; k < Card.cardChars.Length; k++)
            {
                if(Card.cardChars[k] == nowCardChar)
                {
                    this.cardHands[k]++;
                    break;
                }
            }
        }

        int maxCardNum = CharGetMax(cardHands);

        // �÷��� �Ǻ�.
        if (ColorGetMax(cardColors))
        {
            this.towerCardHand = (int)CardHand.Flush;
            // ��Ƽ�� �Ǻ�
            if (cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)CardHand.RoyalStraightFlush;
            }
            // �齺�� �Ǻ�
            if (cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)CardHand.BackStraightFlush;
            }
            // ��Ʈ�� �Ǻ�
            if (CheckStraight(cardHands))
            {
                this.towerCardHand = (int)CardHand.StraightFlush;
            }
            // ��ī�� �Ǻ�
            if (maxCardNum == 5)
            {
                this.towerCardHand = (int)CardHand.FiveCardFlush;
            }
        }
        else
        {
            this.towerCardHand = (int)CardHand.Top;
            // ��ī �Ǻ�
            if(maxCardNum == 5)
            {
                this.towerCardHand = (int)CardHand.FiveCard;
            }
            // ����ƾ �Ǻ�
            if(cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)CardHand.Mountain;
            }
            // ��ī �Ǻ�
            if(maxCardNum == 4)
            {
                this.towerCardHand = (int)CardHand.FourCard;
            }
            // �齺Ʈ �Ǻ�
            if(cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)CardHand.BackStraight;
            }
            // ��Ʈ �Ǻ�
            if (CheckStraight(cardHands))
            {
                this.towerCardHand = (int)CardHand.Straight;
            }
            // Ʈ���� �Ǻ� - Ǯ�Ͽ콺 �Ǻ�
            if(maxCardNum == 3)
            {
                this.towerCardHand = (int)CardHand.Triple;
                // �ΰ��� ���� �а� �ִٸ� Ǯ�Ͽ콺
                for(int i = 0; i < cardHands.Length; i++)
                {
                    if(cardHands[i] == 2)
                    {
                        this.towerCardHand = (int)CardHand.FullHouse; break;
                    }
                }
            }
            // ����� �Ǻ� - ����� �Ǻ�
            if(maxCardNum == 2)
            {
                int num2 = 0;
                for(int i = 0; i < cardHands.Length; i++)
                {
                    if(cardHands[i] == 2) { num2++; }
                }
                if(num2 == 1)
                {
                    this.towerCardHand = (int)CardHand.OnePair;
                }else if(num2 == 2)
                {
                    this.towerCardHand = (int)CardHand.TwoPair;
                }
            }
        }
        return this.towerCardHand;
    }
    private bool ColorGetMax(int[] cardColors)
    {
        for (int i = 0; i < 4; i++)
        {
            if (cardColors[i] == 5)
            {
                return true;
            }
        }
        return false;
    }
    private int CharGetMax(int[] cardHands)
    {
        int tempMax = 0;
        for(int i = 0; i < 8; i++)
        {
            if(cardHands[i] > tempMax)
            {
                tempMax = cardHands[i];
            }
        }
        return tempMax;
    }
    private bool CheckStraight(int[] cardHands)
    {
        if(cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        if(cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[5] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        if (cardHands[2] == 1 && cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        return false;
    }
}