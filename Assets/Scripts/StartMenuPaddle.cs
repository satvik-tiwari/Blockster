using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuPaddle : MonoBehaviour
{
    //configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;
    // Start is called before the first frame update

    //Cached References
   // StartMenuGameSession gameSession;
    StartMenuBall ball;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked     
       // gameSession = FindObjectOfType<StartMenuGameSession>();
        ball = FindObjectOfType<StartMenuBall>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition.x / Screen.width * screenWidthInUnits);

        //float y = transform.position.y;
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(ball.transform.position.x, minX, maxX);
        transform.position = paddlePos;

    }

   
}
