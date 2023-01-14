using UnityEngine;

public class GridMaker : MonoBehaviour
{
    #region Fields

    [SerializeField] public static int _mapLength;
    [SerializeField] public static int _mapWidth;

    [SerializeField] private int mapLength;
    [SerializeField] private int mapWidth;

    //[SerializeField] private List<GameObject> TerrainType;
    [SerializeField] private GameObject mapTile;

    [SerializeField] private GameObject Grid;

    private int random;

    #endregion Fields

    #region Methods

    // Start is called before the first frame update
    private void Start()
    {
        _mapLength = mapLength;
        _mapWidth = mapWidth;

        FillGrid();
    }

    // Fill the grid with the selected TerrainType
    private void FillGrid()
    {
        //random = Random.Range(0, TerrainType.Count + 1);

        float timer = Time.deltaTime + 5;

        for (int x = 1; x <= mapLength; x++)
        {
            for (int z = 1; z <= mapWidth; z++)
            {
                //GameObject a = Instantiate(TerrainType[random]);
                GameObject a = Instantiate(mapTile);
                // CHANGED FROM TIMES 6 TO TIMES 9
                a.transform.position = new Vector3(x * 9, 0, z * 9);
                //a.transform.localScale = new Vector3(2, 2, 2);
                a.transform.parent = Grid.transform;

                // Safety break
                if (Time.deltaTime > timer) break;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #endregion Methods
}