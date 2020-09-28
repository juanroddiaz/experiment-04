using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private List<FoodLogic> m_foodList;
    [SerializeField] private float m_foodSpawnMin = 0.5f;
    [SerializeField] private float m_foodSpawnMax = 1.0f;
    [SerializeField] private ObjectPoolController m_objectPool;

    private float m_currentTick = 0.0f;
    private float m_timer = 0.0f;
    private Vector3 m_randomRot = new Vector3();

    void Start()
    {
        ResetTick();
    }

    void Update()
    {
        if (m_timer < m_currentTick)
        {
            m_timer += Time.deltaTime;
            return;
        }

        SpawnRandomFood();
        ResetTick();
    }

    private void ResetTick()
    {
        m_timer -= m_currentTick;
        m_currentTick = Random.Range(m_foodSpawnMin, m_foodSpawnMax);
    }

    private void SpawnRandomFood()
    {
        var index = Random.Range(0, m_foodList.Count);
        var foodObj = m_foodList[index];
        m_randomRot.y = Random.Range(0.0f, 360.0f);
        m_randomRot.z = Random.Range(0.0f, 360.0f);
        var food = m_objectPool.Spawn(foodObj.gameObject, transform.position, Quaternion.identity);
        food.transform.eulerAngles = m_randomRot;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
