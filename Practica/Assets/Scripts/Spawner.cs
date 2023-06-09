using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] easyItems;
    [SerializeField] private GameObject[] mediumItems;
    [SerializeField] private GameObject[] hardItems;

    [SerializeField] private float precentEasy;
    [SerializeField] private float precentMedium;
    [SerializeField] private float precentHard;
    [SerializeField] private float precentIncreaseItemEvent;

    public bool isSpawned=false;
    public bool isEnableToOutput=true;
    private GameObject currentItem;

    public static Spawner Instance { get; set; }

    private void Start()
    {
        Instance = this;
        StartCoroutine(DoSpawn());
    }

    void Update()
    {
        if (isEnableToOutput&&isSpawned&&currentItem.GetComponent<Item>().isOutput)
        {
            StartCoroutine(DoSpawn());
        }
    }

    private GameObject randomizerItems() 
    {
        float rndPrecent = Random.Range(0,100);
        Debug.Log(rndPrecent);
        int rndItem;

        if (precentEasy/100 >= rndPrecent/100){
            Debug.Log("Легк");
            rndItem = Random.Range(0, easyItems.Length);
            return easyItems[rndItem];
        }
        else if (precentMedium/100 >= rndPrecent/100){
            Debug.Log("Сред");
            rndItem = Random.Range(0, mediumItems.Length);
            return mediumItems[rndItem];
        }
        else if (precentHard/100 >= rndPrecent/100){
            Debug.Log("Слож");
            rndItem = Random.Range(0, hardItems.Length);
            return hardItems[rndItem];
        }
        return null;
    }

    private void TryIncreaseItemEvent(GameObject item)
    {
        float rndPrecent = Random.Range(0, 100);
        if (precentIncreaseItemEvent / 100 >= rndPrecent / 100){
            Transform transform = item.GetComponent<Transform>();
            transform.localScale *= 1.5f;
        }
      
    }

    private IEnumerator DoSpawn()
    {
        isSpawned = false;
        yield return new WaitForSeconds(1f);
        currentItem = Instantiate(randomizerItems(), transform.position, Quaternion.identity);
        TryIncreaseItemEvent(currentItem);
        currentItem.transform.parent = transform;
        isSpawned = true;

    }
}
