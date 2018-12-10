using System;
using NGC.DataStore.Abstraction.Stores;
using NGC.Models.DataObjects;

namespace NGC.DataStore.Implementation.Stores
{
    public class NoteStore : BaseStore<Note>, INoteStore
    {
        public override string Identifier => "Note";
    }
}
