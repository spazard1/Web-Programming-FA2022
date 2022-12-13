using FinalExam.Models;
using System.Collections.Generic;

namespace FinalExam.Services
{
    public class BoardGameDatabase : IBoardGameDatabase
    {
        public BoardGameDatabase()
        {
            boardGames.Add("Dominion", new BoardGameModel() { Name = "Dominion", Genre = "Deck building", NumberOfPlayers = 4 });
            boardGames.Add("Descent", new BoardGameModel() { Name = "Descent", Genre = "Dungeon Crawl", NumberOfPlayers = 5 });
            boardGames.Add("Libertalia", new BoardGameModel() { Name = "Libertalia", Genre = "Role Selection", NumberOfPlayers = 6 });
            boardGames.Add("Nyet", new BoardGameModel() { Name = "Nyet", Genre = "Trick Taking", NumberOfPlayers = 5 });
        }

        private Dictionary<string, BoardGameModel> boardGames = new Dictionary<string, BoardGameModel>();

        public void Add(BoardGameModel model)
        {
            this.boardGames[model.Name] = model;
        }

        public BoardGameModel Get(string name)
        {
            if (boardGames.ContainsKey(name))
            {
                return boardGames[name];
            }
            return null;
        }

        public bool ContainsKey(string name)
        {
            return boardGames.ContainsKey(name);
        }

        public void Remove(string name)
        {
            boardGames.Remove(name);
        }

        public IEnumerable<BoardGameModel> Values => boardGames.Values;
    }
}
