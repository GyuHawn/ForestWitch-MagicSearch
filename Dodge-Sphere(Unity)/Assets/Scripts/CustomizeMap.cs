using UnityEngine;

public class CustomizeMap : MonoBehaviour
{
    public GameObject[] mapTile; // 맵 타일
    public GameObject[] tilesObjects; // 디자인 프리팹

    void Start()
    {
        for (int i = 0; i < mapTile.Length; i++) // 모든 타일에 적용
        {
            Transform tileTransform = mapTile[i].transform; // 부모 타일의 위치
            Instantiate(RandomObject(), tileTransform.position, Quaternion.identity, tileTransform); // 각각 디자인 프리팹 생성
        }
    }

    GameObject RandomObject() // 랜덤한 디자인 프리팹 설정
    {
        int num = Random.Range(0, tilesObjects.Length);
        return tilesObjects[num];
    }
}
