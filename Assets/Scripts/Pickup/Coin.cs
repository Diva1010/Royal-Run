using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Coin : Pickup
{

    [SerializeField] int scoreAmount = 100;
    ScoreManager scoreManager;

    public void Init(ScoreManager scoreManager) {
        this.scoreManager = scoreManager;
    }
    protected override void OnPickup()
    {
        this.scoreManager.IncreaseScore(scoreAmount);
    }

}
