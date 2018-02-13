using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSetup : MonoBehaviour {

    public Item testBoomerang;
    public Item testCoin;
    public Item testTorch;
    public Item testBag;
    public Item testSword;
    public Inventory invMain, invTest;

    // Use this for initialization
    void Start()
    {
        Debug.Log("Place boomerang at 0,0 | success : " + invMain.TryPlaceItem(testBoomerang, new Vector2Int(0, 0)));
        Debug.Log("Place coin at 1,1 | success : " + invMain.TryPlaceItem(testCoin, new Vector2Int(1, 1)));
        Debug.Log("Place torch at 2,0 | success : " + invMain.TryPlaceItem(testTorch, new Vector2Int(2, 0)));
        Debug.Log("Place moneybag at 3,0 | success : " + invMain.TryPlaceItem(testBag, new Vector2Int(3, 0)));
        Debug.Log("Place sword at 0,2 | success : " + invMain.TryPlaceItem(testSword, new Vector2Int(0, 2)));
        //Debug.Log("Place boomerang at 5,5 | success : " + invMain.TryPlaceItem(testBoomerang, new Vector2Int(5, 5)));
        //Debug.Log("Place coin at 5,5 | success : " + invMain.TryPlaceItem(testCoin, new Vector2Int(5, 5)));
        //Debug.Log("Place torch at 0,0 | success : " + invMain.TryPlaceItem(testTorch, new Vector2Int(0, 0)));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
