﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shatoraj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    }

    public enum ChessPieceMoveTypes
    {
        //Unable to move
        None,

        Forward,
        Diagonally,
        VerticallyAndHorizontally,

        //King - Moves one square in any direction.
        OneSquareInAnyDirection,

        //Queen - Moves any number of squares diagonally, horizontally, or vertically.
        AnyNumberOfSquaresInAnyDirection,

        //Rook - Moves any number of squares horizontally or vertically.

        HorizontallyAndVertically,

        //Bishop - Moves any number of squares diagonally.

        //Knight - Moves in an ‘L-shape,’ two squares in a straight direction, and then one square perpendicular to that.

        //Pawn - Moves one square forward, but on its first move, it can move two squares forward. It captures diagonally one square forward.

    }


    public class ChessServer
    {

    }

    public class ChessBoard
    {
        private List<ChessPiece> chessPieces = new List<ChessPiece>();

    }

    public abstract class ChessPiece
    {
        public ChessPiece()
        {
            ID = Guid.NewGuid().ToString();
        }

        protected int previousMoveCount;
        public string ID { get; set; }

        public Color Color { get; set; }
        public string Name { get; set; }
        public abstract int MoveSquaresCount { get; }
        public abstract bool CanMoveHorizontally { get; }
        public abstract bool CanMoveVertically { get; }
        public abstract bool CanMoveDiagonally { get; }
    }

    public abstract class ChessPlayer
    {
        public abstract ChessMove Move(ChessBoard chessBoard);

    }

    public class BlackChessPlayer : ChessPlayer
    {

    }

    public class WhiteChessPlayer : ChessPlayer
    {

    }

    public class ChessMove
    {
        private string pieceId;
        private ChessCell target;

        public ChessMove()
        {
        }

        public ChessMove(string pieceId, ChessCell target)
        {
            this.PieceToMove = pieceId;
            this.Target = target;
        }

        public string PieceToMove { get => pieceId; set => pieceId = value; }
        public ChessCell Target { get => target; set => target = value; }
    }

    public class ChessCell
    {
        public ChessCell(string cellName, int cellNumber)
        {
            CellName = cellName;
            CellNumber = cellNumber;
        }

        public string CellName { get; set; }
        public int CellNumber { get; set; }
    }
}
