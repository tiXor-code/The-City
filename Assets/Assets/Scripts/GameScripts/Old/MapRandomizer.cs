using UnityEngine;

public class MapRandomizer : MonoBehaviour
{
    #region Fields

    // DECLARE a variable to hold the Start Location GameObject, call it startLocation:
    [SerializeField] private GameObject startLocation;

    // DECLARE a variable to hold the End Location GameObject, call it endLocation:
    [SerializeField] private GameObject endLocation;

    // DECLARE a variable to hold the Current Location GameObject, call it currentLocation:
    [SerializeField] private GameObject currentLocation;

    // DECLARE a variable to hold the prefab for the straight road, call it straightRoad:
    [SerializeField] private GameObject roadStraightDownToUp;

    [SerializeField] private GameObject roadStraightLeftToRight;
    [SerializeField] private GameObject roadCurveDownToLeft;
    [SerializeField] private GameObject roadCurveDownToRight;
    [SerializeField] private GameObject roadCurveLeftToUp;
    [SerializeField] private GameObject roadCurveRightToUp;

    // DECLARE a variable to hold the hierarchy parent, call it Pathway:
    [SerializeField] private GameObject Pathway;

    private int i;

    private bool test = true;

    #endregion Fields

    #region Methods

    private void Start()
    {
        RandomizeStartLocation();
        RandomizeEndLocation();
        Draw();
    }

    private float RandomizeStartLocation()
    {
        // SETUP startLocation as random between 6 and 6 * the mapWidth
        int randomStartLocation = 9 * Random.Range(1, (int)GridMaker._mapWidth);

        // SETUP startLocation as random on the grid
        startLocation.transform.position = new(randomStartLocation, 0.05f, 0);

        // SETUP currentLocation to be the same with the startLocation
        currentLocation.transform.position = startLocation.transform.position;

        // RETURN startLocation:
        return randomStartLocation;
    }

    private float RandomizeEndLocation()
    {
        // SETUP startLocation as random between 6 and 6 * the mapWidth
        int randomEndLocation = 9 * Random.Range(1, (int)GridMaker._mapWidth);

        // SETUP startLocation as random on the grid
        endLocation.transform.position = new(randomEndLocation, 0.05f, (int)(9 * (GridMaker._mapLength - 1)));

        // RETURN startLocation:
        return randomEndLocation;
    }

    private void Draw()
    {
        // IF the current location is not the same as the end location, enter the loop:
        if (endLocation.transform.position.x != currentLocation.transform.position.x && endLocation.transform.position.z != currentLocation.transform.position.z)
        {
            // MOVE one square up:
            currentLocation.transform.position = new(currentLocation.transform.position.x + 9, currentLocation.transform.position.y, currentLocation.transform.position.z);
        }
    }

    private void Update()
    {
    }

    #endregion Methods
}