using UnityEngine;

public class FirstSceneEvent : MonoBehaviour
{
    public GameObject house;
    private void Start()
    {
        EventCenter.Subscribe("OnEnemyOneDeath", HouseAppear);
    }
    private void HouseAppear(object obj)
    {
        house.SetActive(true);
    }
}
