using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    
    public sealed class PlayersSwitcher : ClickedOnCellModule
    {
        [SerializeField]
        private string playerNameOne = "Orc", playerNameTwo = "Knight";
        [SerializeField]
        private byte currentPlayerID = 0;
        [SerializeField]
        private Text outputTurn = null;
        [SerializeField]
        private string colorPlayerOneName = "B8FD00",
        colorPlayerTwoName = "00FFD4";
        [SerializeField]
        private CellsStorage cellsStorage = null;
        [SerializeField] private AudioSource orcSound = null, knightSound = null;


        private Player playerOne, playerTwo;
        private string turnFormatText;
        private OnClickedCell OnCellClick;
        private Animator textTurnAnimator;

        public Player currentPlayer { get; private set; }

        private void InitPlayers()
        {
            playerOne = new Player(0, playerNameOne);
            playerTwo = new Player(1, playerNameTwo);

            currentPlayer = (currentPlayerID == 0) ? playerOne : playerTwo;
        }

        
        private void PrintCurrentTurn()
        {
            turnFormatText = "<b><size=52>TURN</size></b>" + "\n" + FormatPlayerName();

            outputTurn.text = turnFormatText;
        }

        public string FormatPlayerName()
        => "<color=#" + ((currentPlayer.id == 0) ? colorPlayerOneName : colorPlayerTwoName)
        + ">"  + currentPlayer.name + "</color>";
        
        private void ChangeTurn()
        {
            if (currentPlayer.id == playerOne.id)
            {
                orcSound.Play();
                currentPlayer = playerTwo;
            }
            else
            {
                knightSound.Play();
                currentPlayer = playerOne;
            }

            PrintCurrentTurn();
            textTurnAnimator.SetTrigger("Change");
        }

        private void Awake()
        {
            OnCellClick = ChangeTurn;
        }

        private void Start()
        {
            InitPlayers();
            PrintCurrentTurn();
            textTurnAnimator = outputTurn.GetComponent<Animator>();

        }

        public override OnClickedCell GetClickedCell() => OnCellClick;
    }
}