using UnityEngine;

namespace Main
{
    public sealed class CellsStorage : MonoBehaviour
    {
        [SerializeField, HideInInspector,GetComponent]
        private Transform cellsParent = null;

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
                cells[cellIndex].SetPlayerId((byte)(cellIndex + 2));
            }
        }

        public void AddOnCellClickListeners(OnClickedCell onCellClick)
        {
            foreach(Cell cell in cells)
            {
                cell.AddClickListener(onCellClick);
            }
        }

        public void RemoveOnCellClickListeners(OnClickedCell onCellClick)
        {
            foreach (Cell cell in cells)
            {
                cell.RemoveClickListener(onCellClick);
            }
        }

        public void AddOnStatesListeners(OnEnterCell OnEnterCell, OnExitCell OnExitCell)
        {
            foreach (Cell cell in cells)
            {
                cell.AddEnterListener(OnEnterCell);
                cell.AddExitListener(OnExitCell);
            }
        }

        private void Awake()
        {
            InitCells();
        }


    }
}