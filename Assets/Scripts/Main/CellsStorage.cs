using UnityEngine;
using VisualEffects;

namespace Main
{
    public delegate void OnEnterCell(Vector3 pos);
    public delegate void OnExitCell();
    public delegate void OnClickedCell();

    /// <summary>
    /// Cell collection storage
    /// </summary>
    public sealed class CellsStorage : MonoBehaviour
    {
        [SerializeField, HideInInspector,GetComponent]
        private Transform cellsParent = null;
        [SerializeField] private UnitsPool unitsPool = null;

        public Cell[] cells { get; private set; }

        private byte[] cellsID;

        public byte[] GetCellsID()
        {
            for (byte index = 0; index < cells.Length; index++)
            {
                cellsID[index] = cells[index].playerID;
            }

            return cellsID;
        }

        private void InitCells()
        {
            cells = new Cell[cellsParent.childCount];
            cellsID = new byte[cells.Length];

            for (byte cellIndex = 0; cellIndex < cellsParent.childCount; cellIndex++)
            {
                cells[cellIndex] = cellsParent.GetChild(cellIndex).GetComponent<Cell>();
                cells[cellIndex].SetPlayerId((byte)(cellIndex + 2)); // cellIndex + 2 because 0 and 1 identifiers belong to players
            }
        }

        public void AddOnCellClickListeners(OnClickedCell onCellClick)
        {
            foreach(Cell cell in cells)
                cell.AddClickListener(onCellClick);          
        }

        public void RemoveOnCellClickListeners(OnClickedCell onCellClick)
        {
            foreach (Cell cell in cells)
                cell.RemoveClickListener(onCellClick);        
        }

        public void AddOnStatesListeners(OnEnterCell OnEnterCell, OnExitCell OnExitCell)
        {
            foreach (Cell cell in cells)
                cell.AddEnterAndExitListeners(OnEnterCell, OnExitCell);        
        }

        private void Awake()
        {
            InitCells();
        }

        private void Start()
        {
            AddOnStatesListeners(unitsPool.OnEnterCell, unitsPool.OnExitCell);
        }
    }
}