using UnityEngine;

/*
 * This class detects Pacman eating Player and controls game behaviour accordingly
 */
public class GameLoose : MonoBehaviour
{
    public Transform player;
    public Transform pacman;
    public Points points;
    public GamePlayer playerMouseScript;
    public PacmanFollow pacmanFollowScript;
    public GameObject messageObject;
    public float initialDelay = 5.0f;
    public float loosingTime = 3.0f;
    public float maxEatDistance = 2.0f;

    float initialDelayLeft = 0.0f;
    float loosingTimeLeft = 0.0f;

    /*
     * Initialize the game
     */
    void Start()
    {
        NewGame();
    }

    /*
     * Control intial countdown
     * Control loose countdown
     * Detect Pacman eating Player
     * Control other behaviours
     */
    void Update()
    {
        if (initialDelayLeft > 0)
        {
            initialDelayLeft -= Time.deltaTime;
            messageObject.GetComponent<TextMesh>().text = ((int)initialDelayLeft + 1).ToString();
            messageObject.SetActive(true);
            points.enabled = false;
        }
        else
        {
            if (loosingTimeLeft > 0)
            {
                loosingTimeLeft -= Time.deltaTime;
                playerMouseScript.enabled = false;
                messageObject.GetComponent<TextMesh>().text = "OUCH!!!";
                messageObject.SetActive(true);
                points.enabled = false;
                if (loosingTimeLeft < 0)
                    NewGame();
            }
            else
            {
                pacmanFollowScript.enabled = true;
                messageObject.SetActive(false);
                points.enabled = true;
                if ((player.position - pacman.position).magnitude < maxEatDistance)
                    loosingTimeLeft = loosingTime;
            }
        }

    }

    /*
     * Initialize values
     */
    void NewGame()
    {
        points.Reset();
        initialDelayLeft = initialDelay;
        pacmanFollowScript.enabled = false;
        playerMouseScript.enabled = true;
    }
}
