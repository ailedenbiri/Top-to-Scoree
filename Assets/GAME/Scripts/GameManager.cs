using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

    public int score = 0;
    public int maxAttempts = 10;
    private int currentAttempt = 0;

    public RectTransform canvasRect;
    public Ease uiShakeEase = Ease.OutElastic;

    public TMP_Text scoreText;
    public BallCO ball;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void OnSuccessfulHit()
    {
        score++;
        currentAttempt++;
        UpdateUI();
        CheckGameOver();
        Invoke("ResetBall", 1f);
        canvasRect.DOShakeAnchorPos(0.3f, 20f, 10, 90f).SetEase(uiShakeEase);
    }

    public void OnMiss()
    {
        currentAttempt++;
        UpdateUI();
        CheckGameOver();
        Invoke("ResetBall", 1f);
    }

    void UpdateUI()
    {
        scoreText.text = $"Skor: {score} | Kalan Deneme: {maxAttempts - currentAttempt}";
    }

    void ResetBall()
    {
        ball.ResetBall();
    }

    void CheckGameOver()
    {
        if (currentAttempt >= maxAttempts)
        {
            Debug.Log("Oyun Bitti!");
            // Buraya kazandı/kaybetti ekranları gelecek
        }
    }
}
