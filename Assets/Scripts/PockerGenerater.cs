using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CardHand
{
    Flush, // 데미지 : 75
    RoyalStraightFlush, // 데미지 : 180
    BackStraightFlush, // 데미지 : 160 
    StraightFlush, // 데미지 : 140
    FiveCardFlush, // 데미지 : 120
    FiveCard, // 데미지 : 100
    Mountain, // 데미지 : 80
    FourCard, // 데미지 : 70
    BackStraight, // 데미지 : 65
    Straight, // 데미지 : 60
    FullHouse, // 데미지 : 50
    Triple, // 데미지 : 40
    TwoPair, // 데미지 : 30
    OnePair, // 데미지 : 20
    Top // 데미지 : 10
};

public class PockerGenerater : MonoBehaviour
{


    [SerializeField]
    private TMP_Text[] cardPlaces;
    private Card[] cardArray;
    private int towerCardHand = 0;
    private int[] cardColors; // 빨, 파, 초, 노 
    private int[] cardHands; // 7, 8, 9, 10, K, Q, J, A

    private void Awake()
    {
        cardArray = new Card[5];
    }


    // 새로운 카드 5개 생성.
    public void NewCards()
    {
        for(int i = 0; i < 5; i++)
        {
            cardArray[i] = new Card();
            cardPlaces[i].SetText(cardArray[i].CardChar);
            cardPlaces[i].color = cardArray[i].CardColor;
        }
    }

    // 카드 변경 버튼 클릭시, 카드 변경.
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

        // 플러쉬 판별.
        if (ColorGetMax(cardColors))
        {
            this.towerCardHand = (int)CardHand.Flush;
            // 로티플 판별
            if (cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)CardHand.RoyalStraightFlush;
            }
            // 백스플 판별
            if (cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)CardHand.BackStraightFlush;
            }
            // 스트플 판별
            if (CheckStraight(cardHands))
            {
                this.towerCardHand = (int)CardHand.StraightFlush;
            }
            // 파카플 판별
            if (maxCardNum == 5)
            {
                this.towerCardHand = (int)CardHand.FiveCardFlush;
            }
        }
        else
        {
            this.towerCardHand = (int)CardHand.Top;
            // 파카 판별
            if(maxCardNum == 5)
            {
                this.towerCardHand = (int)CardHand.FiveCard;
            }
            // 마운틴 판별
            if(cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)CardHand.Mountain;
            }
            // 포카 판별
            if(maxCardNum == 4)
            {
                this.towerCardHand = (int)CardHand.FourCard;
            }
            // 백스트 판별
            if(cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[7] == 1)
            {
                this.towerCardHand = (int)CardHand.BackStraight;
            }
            // 스트 판별
            if (CheckStraight(cardHands))
            {
                this.towerCardHand = (int)CardHand.Straight;
            }
            // 트리플 판별 - 풀하우스 판별
            if(maxCardNum == 3)
            {
                this.towerCardHand = (int)CardHand.Triple;
                // 두개가 같은 패가 있다면 풀하우스
                for(int i = 0; i < cardHands.Length; i++)
                {
                    if(cardHands[i] == 2)
                    {
                        this.towerCardHand = (int)CardHand.FullHouse; break;
                    }
                }
            }
            // 투페어 판별 - 투페어 판별
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

    // 색깔 최대수 체크.
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
    // 카드 같은거 최대수 체크.
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
        // J 스트
        if(cardHands[0] == 1 && cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        // Q 스트
        if(cardHands[1] == 1 && cardHands[2] == 1 && cardHands[3] == 1 && cardHands[5] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        // K 스트
        if (cardHands[2] == 1 && cardHands[3] == 1 && cardHands[4] == 1 && cardHands[5] == 1 && cardHands[6] == 1)
        {
            return true;
        }

        return false;
    }

   
}