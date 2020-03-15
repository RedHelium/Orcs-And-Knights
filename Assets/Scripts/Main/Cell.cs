using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{

    public sealed class Cell : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region CONSTANTS
        private readonly string EMISSION_KEYWORD = "_EMISSION";
        private readonly string EMISSION_COLOR_KEYWORD = "_EmissionColor";
        private readonly Color HIGHLIGHT_COLOR_CELL = Color.red;
        private readonly Vector3 UNIT_OFFSET = new Vector3(0, 2, 0);
        #endregion

        [SerializeField, HideInInspector, GetComponent]
        private MeshRenderer meshRenderer = null;

        public byte playerID { get; private set; }

        private bool isClicked;
        private OnEnterCell OnEnterCell;
        private OnExitCell OnExitCell;
        private event OnClickedCell ClickedEvent;


        public void SetPlayerId(byte id) => playerID = id;

        public void AddClickListener(OnClickedCell ClickedEvent)
        => this.ClickedEvent += ClickedEvent;

        public void RemoveClickListener(OnClickedCell ClickedEvent)
        => this.ClickedEvent -= ClickedEvent;

        public void AddEnterAndExitListeners(OnEnterCell OnEnterCell, OnExitCell OnExitCell)
        {
            this.OnEnterCell = OnEnterCell;
            this.OnExitCell = OnExitCell;
        }

        public void OnPointerDown(PointerEventData eventData) => ClickCellState();    

        public void OnPointerEnter(PointerEventData eventData) => ChangeCellState(true);    

        public void OnPointerExit(PointerEventData eventData) => ChangeCellState(false);
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (isClicked) return;

            ChangeCellState(false);

            isClicked = true;

            eventData.selectedObject = gameObject;
            ClickedEvent.Invoke();
        }
        
        /// <summary>
        /// Function for enter and exit pointer
        /// </summary>
        /// <param name="isEnter"></param>
        private void ChangeCellState(bool isEnter)
        {
            if (isClicked) return;

            if (isEnter)
            {
                meshRenderer.material.EnableKeyword(EMISSION_KEYWORD);
                OnEnterCell(transform.position + UNIT_OFFSET); // Show model over cell
            }
            else
            {
                meshRenderer.material.DisableKeyword(EMISSION_KEYWORD);
                OnExitCell();
            }
        }

        private void ClickCellState()
        {
            if (isClicked) return;

            meshRenderer.material.SetColor(EMISSION_COLOR_KEYWORD, HIGHLIGHT_COLOR_CELL);
        }
  
    }
}