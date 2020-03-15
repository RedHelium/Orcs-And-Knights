using UnityEngine;
using UnityEngine.EventSystems;
using Main;

namespace VisualEffects
{ 
    public sealed class UnitsPool : ClickedOnCellModule
    {
        private readonly byte AMOUNT_UNITS = 5;

        [SerializeField] private GameObject orcReference = null, knightReference = null;
        [SerializeField] private Transform unitsParent = null;
        [SerializeField] private PlayersSwitcher playersSwitcher = null;
        [SerializeField] private EventSystem eventSystem = null;

        private GameObject[] orcs;
        private GameObject[] knights;
        private byte currentOrcIndex;
        private byte currentKnightIndex;
        public OnEnterCell OnEnterCell { get; private set; }
        public OnExitCell OnExitCell { get; private set; }
        private OnClickedCell OnClickedCell;
        private GameObject currentObject;

        public override OnClickedCell GetClickedCell() => OnClickedCell;


        private GameObject GetCurrentOrc() => orcs[currentOrcIndex];
        private GameObject GetCurrentKnight() => knights[currentKnightIndex];

        private void ChangeCurrentObjectState(bool state) => currentObject.SetActive(state);

        private void EnterObjectState(Vector3 pos)
        {
            currentObject = playersSwitcher.currentPlayer.Equals(0) ? GetCurrentOrc() 
            : GetCurrentKnight();

            ChangeCurrentObjectState(true);
            currentObject.transform.position = pos;

        }

        private void ExitObjectState() => ChangeCurrentObjectState(false);

        private void ClickedObjectState()
        {
            eventSystem.currentSelectedGameObject.GetComponent<Cell>().SetPlayerId(playersSwitcher.currentPlayer.ID);

            ChangeCurrentObjectState(true);
            currentObject.GetComponent<Unit>().Activate();

            _ = playersSwitcher.currentPlayer.Equals(0) ? currentOrcIndex++ : currentKnightIndex++;
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
            OnEnterCell = EnterObjectState;
            OnExitCell = ExitObjectState;
        }

        private void Start()
        {
            InitObjects();
        }
    }
}
