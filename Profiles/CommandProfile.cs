using AutoMapper;
using CommanderREST.Dtos;
using CommanderREST.Models;

namespace CommanderREST.Profiles
{
    public class CommandsProfile:Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandDto> ();
            CreateMap<CommandDto, Command>();
           
        }
    }
}