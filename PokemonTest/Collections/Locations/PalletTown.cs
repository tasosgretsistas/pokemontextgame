using System;

namespace PokemonTextEdition.Locations
{
    [Serializable]
    class PalletTown : Location
    {
        public PalletTown()
            : base()
        {
            Name = "Pallet Town";
            Type = LocationType.Town;
            Tag = "pallet";

            North = "route1";

            Description = "the white city of begginings";
            LongDescription = "This cozy little town is full of young boys and girls eager to embark on\ntheir own Pokemon adventure. Professor Oak's lab and your house are here.";
            ConnectionsMessage = "Route 1 lies a bit to the north of here.";            
            HelpMessage = "\"north\" or \"go north\" - moves you to Route 1.\n\"heal\" - has your mum heal your Pokemon.\n\"oak\" or \"oak's lab\" - has Professor Oak assess your Pokedex. (NYI)";
        }

        public override void GoNorth()
        {
        }
    }
}
