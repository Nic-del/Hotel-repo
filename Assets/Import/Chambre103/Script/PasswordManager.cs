using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    public int cubeCount = 4;

    public int[] cubesState;

    public int[] correctState; // le vrai mot de passe

    public DoorLock door;



    void Start()
    {
        cubesState = new int[cubeCount];

        // tous les cubes vides
        for(int i = 0; i < cubesState.Length; i++)
            cubesState[i] = -1;
    }

    


    public void SetCube(int cubeID, int ballNumber)
    {
        cubesState[cubeID] = ballNumber;

        Debug.Log("Etat cubes : " + string.Join(", ", cubesState));

        CheckPassword();
    }

    public void ClearCube(int cubeID)
    {
        cubesState[cubeID] = -1;

        Debug.Log("Cube " + cubeID + " vidé");
    }

    void CheckPassword()
    {
        for(int i = 0; i < cubesState.Length; i++)
        {
            if(cubesState[i] != correctState[i])
                return;
        }

        Debug.Log("✅ Mot de passe correct !");
        Unlock();
    }

    void Unlock()
    {
        Debug.Log("✅ Porte ouverte");
        door.UnlockDoor();
    }

}
