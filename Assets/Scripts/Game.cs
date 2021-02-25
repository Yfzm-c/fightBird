using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	// Use this for initialization
    public enum GAME_STATUS
    {
        Ready,
        InGame,
        GameOver
    }

    private GAME_STATUS status;

    private GAME_STATUS Status
    {
        get { return status; }
        set {
            this.status = value;
            this.UpdateUI();
        }
    }

    public GameObject panelReady;
    public GameObject panelInGame;
    public GameObject panelGameOver;

    public Player player;

    public int score;
    public Text uiScore;
    public Text uiScore2; 
    public int Score
    {
        get { return score; }
        set
        {
            this.score = value;
            this.uiScore.text = this.score.ToString();
            this.uiScore2.text = this.score.ToString();
        }
    }

    public PipelineManger pipelineManger;

	void Start () {
        this.panelReady.SetActive(true);
        this.Status = GAME_STATUS.Ready;
        this.player.OnDeath += Player_OnDeath;
        this.player.OnScore = OnPlayerScore;

    }

    void OnPlayerScore(int score)
    {
        this.Score += score;
    }

    private void Player_OnDeath()
    {
        this.Status = GAME_STATUS.GameOver;
        this.pipelineManger.Stop();
    }

    // Update is called once per frame
    void Update () {
		
	}
    
    public void StartGame()
    {
        this.Status = GAME_STATUS.InGame;
        Debug.LogFormat("StartGame:{0}",this.status);

        pipelineManger.StartRun();
        player.Fly();
    }

    public void UpdateUI()
    {
        this.panelReady.SetActive(this.Status == GAME_STATUS.Ready);
        this.panelInGame.SetActive(this.Status == GAME_STATUS.InGame);
        this.panelGameOver.SetActive(this.Status == GAME_STATUS.GameOver);
    }

    public void Restart()
    {
        this.Status = GAME_STATUS.Ready;
        this.pipelineManger.Init();
        this.player.Init();
    }

}
