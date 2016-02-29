using System;

namespace PokemonTextEdition.Engine
{
    enum Platform
    {
        None,
        Console,
        Windows,
        Mobile
    }

    class Settings
    {       

        /// <summary>
        /// The current version of the game in a string format.
        /// </summary>
        public const string Game_Version = "v0.2 [BETA]";

        /// <summary>
        /// The platform that the game is running on, i.e. Console or mobile.
        /// </summary>
        public const Platform Game_Platform = Platform.Console;

        /// <summary>
        /// The developer mode setting of the game.
        /// </summary>
        public static bool Game_GodMode = true;

        /// <summary>
        /// This parameter determines how important a message needs to be in order to be logged.
        /// 0 = trivial, 1 = important, 2 = vital.
        /// </summary>
        public const int Program_LogLevel = 1;

        /// <summary>
        /// The color of the font of general messages in the Windows console. (NYI)
        /// </summary>
        public static ConsoleColor Program_FontColor = ConsoleColor.Yellow;

        #region Battle-related

        /// <summary>
        /// The chance that an attack will be a critical hit, expressed in %.
        /// </summary>
        public const int Battle_CriticalStrikeChance = 10;

        /// <summary>
        /// The amount of extra damage damage that a critical hit will deal, expressed in float - i.e. 1.2 for 20% extra damage.
        /// </summary>
        public const float Battle_CriticalStrikeDamageMultiplier = 1.2f;

        /// <summary>
        /// The chance that a paralyzed Pokemon will be able to attack, expressed in %.
        /// </summary>
        public const int Battle_ParalysisAttackChance = 75;

        #endregion
    }
}
