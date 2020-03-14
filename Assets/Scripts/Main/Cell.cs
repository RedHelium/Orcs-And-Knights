using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{

    public sealed class Cell : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        private readonly string EMISSION_KEYWORD = "_EMISSION";
        private readonly string EMISSION_COLOR_KEYWORD = "_EmissionColor";
        private readonly Color HIGHLIGHT_COLOR_CELL = Color.red;
        private readonly Vector3 UNIT_OFFSET = new Vector3(0, 2, 0);

        [SerializeField, HideInInspector, GetComponent]
        private MeshRenderer meshRenderer = null;

        public byte playerID { get; private set; }
        private bool isClicked;
        private OnEnterCell OnEnterCell;
        private OnExitCell OnExitCell;
        private event OnClickedCell ClickedEvent;

        public void AddClickListener(OnClickedCell onClickedCell)
        => ClickedEvent += onClickedCell;

        public void RemoveClickListener(OnClickedCell onClickedCell)
        => ClickedEvent -= onClickedCell;

        public void AddEnterListener(OnEnterCell OnEnterCell)
        => this.OnEnterCell = OnEnterCell;
        public void AddExitListener(OnExitCell OnExitCell)
        => this.OnExitCell = OnExitCell;


        public void SetPlayerId(byte id) => playerID = id;

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickCellState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeCellState(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeCellState(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isClicked) return;

            ChangeCellState(false);

            isClicked = true;

            eventData.selectedObject = gameObject;
            ClickedEvent.Invoke();
        }
        
        /// <summary>
        ///  "state" true if enter state
        /// </summary>
        /// <param name="state"></param>
        private void ChangeCellState(bool state)
        {
            if (isClicked) return;

            if (state)
            {
                meshRenderer.material.EnableKeyword(EMISSION_KEYWORD);
                OnEnterCell(transform.position + UNIT_OFFSET);
            }
            else
            {
                meshRenderer.material.DisableKeyword(EMISSION_KEYWORD);
                OnExitCell();
            }
        }

        /// <summary>
        /// "state" true if cell clicked
        /// </summary>
        private void ClickCellState()
        {
            if (isClicked) return;

            meshRenderer.material.SetColor(EMISSION_COLOR_KEYWORD, HIGHLIGHT_COLOR_CELL);
        }


       
    }
}