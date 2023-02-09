using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

[DefaultExecutionOrder(105)]

public class TileAnimManager : MonoBehaviour
{
    //References
    [Header("UI references")]
    [SerializeField] TMP_Text coinUIText;
    [SerializeField] public GameObject animatedCoinPrefab;
    [SerializeField] RectTransform target;

    [Space]
    [Header("Available tiles : (tiles to pool)")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();


    [Space]
    [Header("Animation settings")]
    [SerializeField][Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField][Range(0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;
    [SerializeField] float spread;
    public static TileAnimManager instance;

    Vector3 targetPosition;

    private int _c = 0;

    public int Coins
    {
        get { return _c; }
        set
        {
            _c = value;
            //update UI text whenever "Coins" variable is changed
            coinUIText.text = Coins.ToString();
        }
    }

    void Awake()
    {
        SetImage();
        PrepareCoins();
        instance = this;
    }
   

    void PrepareCoins()
    {
        SetImage();
        GameObject coin;
        for (int i = 0; i < maxCoins; i++)
        {
            coin = Instantiate(animatedCoinPrefab);
            coin.transform.parent = transform;
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }

    void Animate(Vector3 collectedCoinPosition, int amount)
    {
        
        for (int i = 0; i < amount; i++)
        {
            //check if there's coins in the pool
            if (coinsQueue.Count > 0)
            {
                //extract a coin from the pool
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);

                //move coin to the collected coin pos
                coin.transform.position = collectedCoinPosition + new Vector3(UnityEngine.Random.Range(-spread, spread), 0f, 0f);

                //animate coin to target position
                float duration = UnityEngine.Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration)
                .SetEase(easeType)
                .OnComplete(() => {
                    //executes whenever coin reach target position
                    coin.SetActive(false);
                    coinsQueue.Enqueue(coin);

                    string strgoal = PlayAreaController.instance.goal.text;
                    int goal = Int32.Parse(strgoal);
                    goal--;

                    PlayAreaController.instance.goal.text = goal.ToString();

                    
                });
            }
        }
    }

    public void AddCoins(Vector3 collectedCoinPosition, int amount)
    {
        
        targetPosition = target.position;
        Animate(collectedCoinPosition, amount);
    }

    public void SetImage()
    {
        animatedCoinPrefab.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Goal").GetComponent<Image>().sprite;
    }

}
