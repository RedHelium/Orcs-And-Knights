using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{

    public delegate void OnEnterCell(Vector3 pos);
    public delegate void OnExitCell();
    public delegate void OnClickedCell();

    public sealed class UnitsPool : ClickedOnCellModule
    {
        private readonly byte AMOUNT_UNITS = 5;

        [SerializeField] private GameObject orcReference = null, knightReference = null;
        [SerializeField] private Transform unitsParent = null;
        [SerializeField] private CellsStorage cellsStorage = null;
        [SerializeField] private PlayersSwitcher playersSwitcher = null;
        [SerializeField] private EventSystem eventSystem = null;


        private GameObject[] orcs;
        private GameObject[] knights;
        private byte currentOrcIndex;
        private byte currentKnightIndex;
        private OnEnterCell OnEnterCell;
        private OnExitCell OnExitCell;
        private OnClickedCell OnClickedCell;
        private GameObject currentObject;

        private GameObject GetCurrentOrc() => orcs[currentOrcIndex];
        private GameObject GetCurrentKnight() => knights[currentKnightIndex];

        private void EnterObjectState(Vector3 pos)
        {
            currentObject = (playersSwitcher.currentPlayer.id == 0) ?
            GetCurrentOrc() : GetCurrentKnight();

            currentObject.SetActive(true);
            currentObject.transform.position = pos;

        }

        private void ExitObjectState()
        {
            currentObject.SetActive(false);
        }

        private void ClickedObjectState()
        {
            eventSystem.currentSelectedGameObject.GetComponent<Cell>().SetPlayerId(playersSwitcher.currentPlayer.id);
            currentObject.SetActive(true);
            currentObject.GetComponent<Rigidbody>().useGravity = true;
            currentObject.transform.GetChild(0).GetComponent<Animator>().enabled = true;

            _ = (playersSwitcher.currentPlayer.id == 0) ? currentOrcIndex++ : currentKnightIndex++;
        }

        private void InitObjects()
        {
            orcs = new GameObject[AMOUNT_UNITS];
            knights = new GameObject[AMOUNT_UNITS];

            for (byte index = 0; index < AMOUNT_UNITS; index++)
            {
                orcs[index] = Instantiate(orcReference, unitsParent);
                knights[index] = Instantiate(knightReference, unitsParent);

                orcs[index].SetActive(false);
                knights[index].SetActive(false);
            }
        }

        private void Awake()
        {
            OnClickedCell = ClickedObjectState;
        }

        private void Start()
        {
            InitObjects();

            OnEnterCell = EnterObjectState;
            OnExitCell = ExitObjectState;

            cellsStorage.AddOnStatesListeners(OnEnterCell, OnExitCell);
        }

        public override OnClickedCell GetClickedCell() => OnClickedCell;
        
    }
}
