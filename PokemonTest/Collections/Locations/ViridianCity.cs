using PokemonTextEdition.Classes;
using PokemonTextEdition.Collections;
using PokemonTextEdition.Engine;

namespace PokemonTextEdition.Locations
{
    class ViridianCity : Location
    {
        public ViridianCity()
        {
            Name = "Viridian City";
            Type = LocationType.City;
            Tag = LocationTag.ViridianCity;

            South = LocationTag.Route1;
            North = LocationTag.Route2South;

            FlavorMessage = "the evergreen city";

            Description = "This bustling city is most young trainers' first stop. There is a Pokemon\n" +
                          "Center to heal your Pokemon, as well as a Mart where you can buy items.\n" +
                          "The Viridian Gym is also located here, but it is currently closed.";

            ConnectionsMessage = "Route 1 is just south off here, and Route 2 within a few minutes to the north.\n" +
                                 "To the west lies Route 22, the gateway to Indigo Plateau.";

            HelpMessage = "\"north\" or \"go north\" - moves you to Route 2.\n" +
                          "\"south\" or \"go south\" - moves you to Route 1.\n" +
                          "\"center\" or \"heal\" - takes you to a Pokemon center to heal your Pokemon.\n" +
                          "\"mart\" - takes you to a Pokemon mart where you can buy and sell items.";

            MartStock.Add(ItemList.pokeball);
            MartStock.Add(ItemList.potion);
            MartStock.Add(ItemList.antidote);
            MartStock.Add(ItemList.awakening);
        }

        public override void GoNorth()
        {
        }

        public override void GoSouth()
        {
        }

        public override void GoWest()
        {
            UI.WriteLine("This location is not currently in the game.\n");

            Overworld.LoadLocation(LocationTag.ViridianCity);
        }
    }
}
