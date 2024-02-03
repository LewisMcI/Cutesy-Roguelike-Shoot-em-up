using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    private void Awake()
    {
        playerStats.OnLevelUp.AddListener(LevelUp);
        LevelUp();
    }

    enum CardType
    {
        AtkDmgUp,
        AtkSpdUp,
        BulletSpdUp
    }

    [SerializeField] private Transform[] cardParents;

    [SerializeField] private GameObject atkDmgUpPrefab;
    [SerializeField] private GameObject atkSpdUpPrefab;
    [SerializeField] private GameObject bulletSpdUpPrefab;

    CardType[] cards = new CardType[3];

    void LevelUp()
    {
        cardParents[0].parent.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            // Destroy curr card(s)
            for (int j = 0; j < cardParents[i].childCount; j++)
            {
                Destroy(cardParents[i].GetChild(j).gameObject);
            }

            // Generate Card
            Array values = Enum.GetValues(typeof(CardType));
            System.Random random = new System.Random();
            CardType randomCard = (CardType)values.GetValue(random.Next(values.Length));
            cards[i] = randomCard;

            // Spawn Card
            switch (randomCard)
            {
                case CardType.AtkDmgUp:
                    Instantiate(atkDmgUpPrefab, cardParents[i].position, Quaternion.identity, cardParents[i]);
                    break;
                case CardType.AtkSpdUp:
                    Instantiate(atkSpdUpPrefab, cardParents[i].position, Quaternion.identity, cardParents[i]);
                    break;
                case CardType.BulletSpdUp:
                    Instantiate(bulletSpdUpPrefab, cardParents[i].position, Quaternion.identity, cardParents[i]);
                    break;
                default:
                    Debug.Log("Undefined Card Type");
                    break;
            }
        }
    }

    public void ActivateCard(int card)
    {
        if (card >= cards.Length || card < 0)
            Debug.Log("Card Num invalid");


        switch (cards[card])
        {
            case CardType.AtkDmgUp:
                playerStats.AddAtkDmgMultiplier(2);
                break;
            case CardType.AtkSpdUp:
                playerStats.AddAtkSpdMultiplier(2);
                break;
            case CardType.BulletSpdUp:
                playerStats.AddBulletSpdMultiplier(2);
                break;
            default:
                Debug.Log("Undefined Card Type");
                break;
        }
    }
}
