using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    
    public sealed class PlayersSwitcher : ClickedOnCellModule
    {
        private readonly string TURN_HEADER = "<b><size=52>TURN</size></b> \n";

        [SerializeField] private string playerNameOne = "Orc", playerNameTwo = "Knight";
        [SerializeField] private byte currentPlayerID = 0;
        [SerializeField] private Text outputTurn = null;
        [SerializeField] private string colorPlayerOneName = "B8FD00", 
        colorPlayerTwoName = "00FFD4";
        [SerializeField] private AudioSource orcSound = null, knightSound = null;

        private Player playerOne, playerTwo;
        private OnClickedCell OnCellClick;
        private Animator textTurnAnimator;

        public Player currentPlayer { get; private set; }

        public override OnClickedCell GetClickedCell() => OnCellClick;


        private void InitPlayers()
        {
            playerOne = new Player(0, playerNameOne);
            playerTwo = new Player(1, playerNameTwo);

            currentPlayer = (currentPlayerID == 0) ? playerOne : playerTwo;
        }

        private void PrintCurrentTurn() => outputTurn.text = TURN_HEADER + FormatPlayerName();
        
        public string FormatPlayerName()
        => "<color=#" + ((currentPlayer.ID == 0) ? colorPlayerOneName : colorPlayerTwoName)
        + ">"  + currentPlayer.Name + "</color>";
        
        private void ChangeTurn()
        {

            if (currentPlayer.Equals(playerOne.ID))
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
    }
}