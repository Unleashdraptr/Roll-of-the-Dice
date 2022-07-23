using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsAndUI : MonoBehaviour
{
    public Transform[] Players;
    public GameObject PlayerStorage;
    public GameObject Tiles;
    public GameObject collisions;

    public Transform[] OldPos;
    public int TargetNum;
    public GameObject UIcontrol;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        Players =  PlayerStorage.GetComponentsInChildren<Transform>();
    }
    public void OnMoveButton()
    {
        if (collisions.gameObject.activeSelf == true)
        {
            collisions.gameObject.SetActive(false);
        }
        if (Players[TargetNum-1].GetComponent<Stats>().TurnSpent == false)
        {
            UIcontrol.GetComponent<Raycast>().OnTarget = true;
            for (int i = 0; i < Tiles.transform.childCount; i++)
            {
                if (Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanMoveTo == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == false || Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().TakenID == TargetNum)
                {
                    Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                }
            }
            GameObject.Find("Move").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("Moving").transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void OnConfirmButton()
    {
        UIcontrol.GetComponent<Raycast>().OnTarget = false;
        GameObject.Find("Moved").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
    public void OnBattleButton()
    {
        collisions.gameObject.SetActive(false);
        int Childnum = 0;
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            if (Tiles.transform.GetChild(i).transform.position == Players[TargetNum].transform.position)
            {
                Childnum = i;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            if (Tiles.transform.GetChild(Childnum).GetComponent<CanWalkTo>().IsTaken == true && Tiles.transform.GetChild(Childnum).GetComponent<CanWalkTo>().TakenID <= 4)
            {

            }
        }
    }

    public void OnWaitButton()
    {
        Players[TargetNum-1].GetComponent<Stats>().TurnSpent = true;
        OldPos[TargetNum-1] = Players[TargetNum-1].transform;
        GameObject.Find("Moved").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        UIcontrol.GetComponent<Raycast>().OnTarget = false;
    }
    public void MoveConfirmCancelButton()
    {
        //Resets previous places where they player could attack
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanAttack = false;
        }
        UIcontrol.GetComponent<Raycast>().OnTarget = true;
        GameObject.Find("Moving").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Moved").transform.localScale = new Vector3(0, 0, 0);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            if (Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanMoveTo == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == false || Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().TakenID == TargetNum)
            {
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
        }
    }
    public void MovingCancelButton()
    {
        //Sets up the aproppiate buttons for use
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);


        Debug.Log("Move");
        Players[TargetNum].GetComponent<CharacterMovement>().TargetPosition = OldPos[TargetNum-1];
        //Resets all tiles that can be moved to and what players the scripts will use
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        UIcontrol.GetComponent<Raycast>().OnTarget = false;
    }
}