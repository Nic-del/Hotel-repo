using UnityEngine;

public class PasswordCube : MonoBehaviour
{
    public int cubeID;
    private PasswordManager manager;
    private bool triggered = false;

    void Start()
    {
        manager = FindObjectOfType<PasswordManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(triggered) return;


        PasswordBall ball = other.GetComponent<PasswordBall>();

        if(ball != null && ball.targetCubeID == cubeID)
        {

                    Debug.Log("Cube " + cubeID + 
          " touche boule cible = " + ball.targetCubeID);
            triggered = true;
            manager.SetCube(cubeID, ball.ballNumber);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PasswordBall ball = other.GetComponent<PasswordBall>();

        if(ball != null && ball.targetCubeID == cubeID)
        {
            triggered = false;
            manager.ClearCube(cubeID);
        }
    }
}
