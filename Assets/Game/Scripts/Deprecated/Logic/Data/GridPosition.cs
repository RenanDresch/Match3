namespace Game.Logic
{
    public class GridPosition
    {
        #region Properties

        public int Row { get; set; }
        public int Column { get; set; }

        #endregion

        #region Public Methods

        public void Swap(GridPosition incoming)
        {
            var incomingColumn = incoming.Column;
            var incomingRow = incoming.Row;

            incoming.Column = Column;
            incoming.Row = Row;

            Column = incomingColumn;
            Row = incomingRow;
        }

        #endregion
    }
}