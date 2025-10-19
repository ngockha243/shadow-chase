namespace _0.Game.Scripts
{
    public static class GameLevel
    {
        public static bool firstTimeShowTutorial = false;
        public enum MapGame
        {
            Map_1, Map_2, Map_3, Map_4, Map_5, Map_6
        }

        public static MapGame currentMap = MapGame.Map_1;
    }
}