using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PockerGenerater : MonoBehaviour
{
    private enum cardHand
    {
       Flush, // ������ : 75
       RoyalStraightFlush, // ������ : 180
       BackStraightFlush, // ������ : 160 
       StraightFlush, // ������ : 140
       FiveCardFlush, // ������ : 120
       FiveCard, // ������ : 100
       Mountain, // ������ : 80
       FourCard, // ������ : 70
       BackStraight, // ������ : 65
       Straight, // ������ : 60
       FullHouse, // ������ : 50
       Triple, // ������ : 40
       TwoPair, // ������ : 30
       OnePair, // ������ : 20
       Top // ������ : 10
    };


    [SerializeField]
    private TMP_Text[] cardPlaces;
    private Card[] cardArray = new Card[5];
    private int towerCardHand = 0;
    private int[] cardColors; // ��, ��, ��, �� 
    private int[] cardHands; // 7, 8, 9, 10, K, Q, J, A


    // ���ο� ī�� 5�� ����.
    public void newCards()
    {
        for(int i = 0; i < 5; i++)
        {
            cardArray[i] = new Card();
            cardPlaces[i].SetText(cardArray[i].CardChar);
            cardPlaces[i].color = cardArray[i].CardColor;
        }
    }

    // ī�� ���� ��ư Ŭ����, ī�� ����.
    public void changeCards(int index)
    {
        cardArray[index] = new Card();
        cardPlaces[index].SetText(cardArray[index].CardChar);
        cardPlaces[index].color = cardArray[index].CardColor;
    }

    public int checkCardHands()
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

        int maxCardNum = chargetMax(cardHands);

        // �÷��� �Ǻ�.
        if (colorgetMax(cardColors))
        {
            this.towerCardHand = (int)cardHand.Flush;
            // ��Ƽ�� �Ǻ�
            if (cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)cardHand.RoyalStraightFlush;
            }
            // �齺�� �Ǻ�
            if (cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)cardHand.BackStraightFlush;
            }
            // ��Ʈ�� �Ǻ�
            if (checkStraight(cardHands))
            {
                this.towerCardHand = (int)cardHand.StraightFlush;
            }
            // ��ī�� �Ǻ�
            if (maxCardNum == 5)
            {
                this.towerCardHand = (int)cardHand.FiveCardFlush;
            }
        }
        else
        {
            this.towerCardHand = (int)cardHand.Top;
            // ��ī �Ǻ�
            if(maxCardNum == 5)
            {
                this.towerCardHand = (int)cardHand.FiveCard;
            }
            // ����ƾ �Ǻ�
            if(cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)cardHand.Mountain;
            }
            // ��ī �Ǻ�
            if(maxCardNum == 4)
            {
                this.towerCardHand = (int)cardHand.FourCard;
            }
            // �齺Ʈ �Ǻ�
            if(cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)cardHand.BackStraight;
            }
            // ��Ʈ �Ǻ�
            if (checkStraight(cardHands))
            {
                this.towerCardHand = (int)cardHand.Straight;
            }
            // Ʈ���� �Ǻ� - Ǯ�Ͽ콺 �Ǻ�
            if(maxCardNum == 3)
            {
                this.towerCardHand = (int)cardHand.Triple;
                // �ΰ��� ���� �а� �ִٸ� Ǯ�Ͽ콺
                for(int i = 0; i < cardHands.Length; i++)
                {
                    if(cardHands[i] == 2)
                    {
                        this.towerCardHand = (int)cardHand.FullHouse; break;
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
                    this.towerCardHand = (int)cardHand.OnePair;
                }else if(num2 == 2)
                {
                    this.towerCardHand = (int)cardHand.TwoPair;
                }
            }
        }
        return this.towerCardHand;
    }

    // ���� �ִ�� üũ.
    private bool colorgetMax(int[] cardColors)
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
    // ī�� ������ �ִ�� üũ.
    private int chargetMax(int[] cardHands)
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
    private bool checkStraight(int[] cardHands)
    {
        // J ��Ʈ
        if(cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        // Q ��Ʈ
        if(cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[5] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        // K ��Ʈ
        if (cardHands[2] == 1 && cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        return false;
    }

   
}