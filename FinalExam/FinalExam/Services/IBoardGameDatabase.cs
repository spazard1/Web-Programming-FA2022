using FinalExam.Models;
using System.Collections.Generic;

namespace FinalExam.Services
{
    public interface IBoardGameDatabase
    {
        void Add(BoardGameModel model);

        BoardGameModel Get(string name);

        void Remove(string name);

        IEnumerable<BoardGameModel> Values { get; }

        bool ContainsKey(string name);
    }
}