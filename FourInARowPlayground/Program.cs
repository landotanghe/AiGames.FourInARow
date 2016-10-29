using FourInARow.Visualisation;

namespace FourInARow
{
    class Program
    {
        public const int Real = 1;
        public const int TestCmd = 2;
        public const int TestGui = 3;

        public static int Mode = TestGui;

        static void Main(string[] args)
        {
            switch (Mode)
            {
                case Real:
                    (new Session()).Run();
                    break;
                case TestCmd:
                    (new PlayInCmd()).PlayBotGames(args);
                    break;
                case TestGui:
                    (new FourInARowFormController()).Init();
                    break;
                default:
                    throw new System.Exception();
            }
        }
    }
}