using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singeleton;
    public bool GameStarted { get; private set; }
    public bool GameEnded { get; private set; }
    public float EntireDistance { get; private set; }
    public float DistanceLeft { get; private set; }
    public int targetFrameRate = 60;

    [SerializeField] private float slowMotionFactor = .1f;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform goalTransform;
    [SerializeField] private BallController ball;
   
    private void Awake()
    {      
     
        if (singeleton == null)
        {
            singeleton = this;
        }
        else if (singeleton != this)
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void Start()
    {
         QualitySettings.vSyncCount = 0;
         Application.targetFrameRate = targetFrameRate;


    EntireDistance = goalTransform.position.z - startTransform.position.z;
        GameStarted = false;
        
    }
    public void EndGame(bool win)
    {
        GameEnded = true;
        if (!win)
        {          
            //Restart the Game
          
        Invoke("RestartGame", 2 * slowMotionFactor);
            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStarted)
        {
            if (Input.GetMouseButton(0))
            {
                GameStarted = true;
                
                
            }
        }
        DistanceLeft = Vector3.Distance(ball.transform.position, goalTransform.position);
            if (DistanceLeft > EntireDistance)
            DistanceLeft = EntireDistance;

        if (ball.transform.position.z > goalTransform.position.z)
            DistanceLeft = 0;
        Debug.Log("Traveled Distance is" + DistanceLeft + "Entire Distance is" + EntireDistance);
    }
   
}
