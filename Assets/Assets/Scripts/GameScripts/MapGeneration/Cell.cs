using UnityEngine;

namespace TheCity.ChessMaze
{
    public enum CellObjectType
    {
        Empty,
        Road,
        Obstacle,
        Start,
        Exit
    }

    public class Cell : MonoBehaviour
    {
        #region Private Fields

        private bool isTaken;
        private CellObjectType objectType;
        private int x, z;

        #endregion Private Fields

        #region Public Constructors

        public Cell(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.objectType = CellObjectType.Empty;
            isTaken = false;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool IsTaken { get => isTaken; set => isTaken = value; }
        public CellObjectType ObjectType { get => objectType; set => objectType = value; }
        public int X { get => x; }
        public int Z { get => z; }

        #endregion Public Properties
    }
}