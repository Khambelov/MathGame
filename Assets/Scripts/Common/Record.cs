using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Record", menuName = "SO/Player Record")]
public class Record : ScriptableObject
{
    [SerializeField]
    BestPlayer[] bestPlayers;

    public void AddBestPlayer(string pName, int winrate, int correctCount, float speed)
    {
        BestPlayer player = new BestPlayer(pName, winrate, correctCount, speed);


        for (int i = 0; i < bestPlayers.Length; i++)
        {
            if (bestPlayers[i].Equals(null))
            {
                bestPlayers[i] = player;
                break;
            }
            else
            {
                if (СompareByWinrate(bestPlayers[i], player))
                {
                    UpdateList(bestPlayers[i], i);

                    bestPlayers[i] = player;

                    break;
                }
            }
        }
    }

    public BestPlayer[] GetPlayerList()
    {
        return bestPlayers;
    }

    bool СompareByWinrate(BestPlayer curPlayer, BestPlayer newPlayer)
    {
        if (newPlayer.winrate > curPlayer.winrate)
        {
            return true;
        }
        else if (newPlayer.winrate == curPlayer.winrate)
        {
            return CompareByAnswers(curPlayer, newPlayer);
        }
        else
        {
            return false;
        }
    }

    bool CompareByAnswers(BestPlayer curPlayer, BestPlayer newPlayer)
    {
        if (newPlayer.correctCount > curPlayer.correctCount)
        {
            return true;
        }
        else if (newPlayer.correctCount == curPlayer.correctCount)
        {
            return CompareBySpeed(curPlayer, newPlayer);
        }
        else
        {
            return false;
        }
    }

    bool CompareBySpeed(BestPlayer curPlayer, BestPlayer newPlayer)
    {
        if (newPlayer.speed > curPlayer.speed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void UpdateList(BestPlayer prevPlayer, int index)
    {
        BestPlayer tempPlayer = prevPlayer;

        for (int i = index + 1; i < bestPlayers.Length; i++)
        {
            tempPlayer = bestPlayers[i];
            bestPlayers[i] = prevPlayer;
            prevPlayer = tempPlayer;
        }
    }
}

[System.Serializable]
public struct BestPlayer
{
    public string pName;
    public int winrate;
    public int correctCount;
    public float speed;

    public BestPlayer(string pName, int winrate, int correctCount, float speed)
    {
        this.pName = pName;
        this.winrate = winrate;
        this.correctCount = correctCount;
        this.speed = speed;
    }
}
