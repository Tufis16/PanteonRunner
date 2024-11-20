using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")]
    public Player playerScript;

    [Header("Camera Settings")]
    public CinemachineVirtualCamera cmVirtualCamera_TPS;
    public CinemachineVirtualCamera cmVirtualCamera_Shoulder;

    [Header("Opponent Settings")]
    public List<Opponent> opponentScripts;

    [Header("UI Elements")]
    public Canvas canvas;
    public Animator canvasAnimator;
    public GameObject panel_DeathPanel, panel_PaintingWall, panel_GameLevelComplete;

    public TMP_Text deathCountText;
    public TMP_Text coinCountText;
    public Transform coinMovePoint;

    [Header("Game")]
    public FinishArea finishArea;
    public float delayRestart = 1f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy any extra instances of GameManager
            Destroy(gameObject); 
            return;
        }

        opponentScripts = new List<Opponent>();
    }

    private void Start()
    {
        //Find player
        playerScript = FindObjectOfType<Player>();

        //Find all opponents / ai
        opponentScripts.AddRange(FindObjectsOfType<Opponent>());

        //Fixing Update Death and Coin textes when start,
        UpdateDeathCountText();
        UpdateCoinCountText();
    }

    //Game Over works here
    public void GameOver()
    {
        Debug.Log("Game Over!");
        panel_DeathPanel.gameObject.SetActive(true);
        canvasAnimator.SetTrigger("Death");

        //Death count raising
        PlayerPrefs.SetInt("DeathCount", PlayerPrefs.GetInt("DeathCount") + 1);

        UpdateDeathCountText();
    }
    //DeathCount text update
    public void UpdateDeathCountText()
    {
        deathCountText.text = PlayerPrefs.GetInt("DeathCount").ToString();
    }

    //Coin count saving
    public void AddCoin(int amount)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + amount);
        UpdateCoinCountText();
    }
    //CoinCount text update
    public void UpdateCoinCountText()
    {
        coinCountText.text = PlayerPrefs.GetInt("Coin").ToString();
    }


    //Make Game finish
    public void GameFinishLine()
    {
        Debug.Log("Reached to finish line!");
        cmVirtualCamera_TPS.gameObject.SetActive(false);
        cmVirtualCamera_Shoulder.gameObject.SetActive(true);

        //Triggering finish move anim
        playerScript.finishLineMovement = true;

        //Panel player stats opening
        canvasAnimator.SetBool("StatsFadeOff", true);

        //Panel painting wall opening
        panel_PaintingWall.SetActive(true);
        canvasAnimator.SetBool("PaintingWallFadeOn", true);

    }

    // Make Game finish
    public void GameLevelComplete()
    {
        Debug.Log("Level Complete!");
        panel_GameLevelComplete.SetActive(true);
        canvasAnimator.SetBool("LevelComplete", true);
    }

    //Game restart with delay
    public void GameRestart()
    {
        canvasAnimator.SetTrigger("Restart");
        StartCoroutine(RestartWithDelay());
        canvasAnimator.SetBool("FadeOn", true);
    }

    private IEnumerator RestartWithDelay()
    {
        yield return new WaitForSeconds(delayRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Game Restarted!");
    }
}
