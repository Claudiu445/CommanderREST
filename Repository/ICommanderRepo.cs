using CommanderREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommanderREST.Repository
{
    public interface ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands();
        public Command GetCommandById(int id);
        void CreateCommand(Command cmd);

        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
        bool SaveChanges();
    }
}
