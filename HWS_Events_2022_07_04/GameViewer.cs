namespace HWS_Events_2022_07_04
{
    public class GameViewer
    {
        //methods
        public void GoodSpaceShipHPChangedEventHandler(object? ob, PointsEventArgs eventArgs)
        {
            Console.WriteLine("\n******************* HP change ********************");

            Console.WriteLine($"\nCurrent HP: {eventArgs.OriginalHitPoints}");
            switch (eventArgs.ExtraHP)
            {
                case 0:
                    {
                        if (eventArgs.Damage != 0)
                        {
                            Console.WriteLine($"Good SpaceShip got damage of {eventArgs.Damage} points");
                        }
                        break;
                    }
                default:
                    {
                        if (eventArgs.ExtraHP != 0)
                        {
                            Console.WriteLine($"Good SpaceShip got extra {eventArgs.ExtraHP} points");
                        }
                        break;
                    }
            }
            Console.WriteLine($"HP after change: {eventArgs.HitPoints}");
            if (eventArgs.HitPoints == eventArgs.MaxHitPoints)
            {
                Console.WriteLine("HP is full!");
            }
        }
        public void GoodSpaceShipLocationChangedEventHandler(object? ob, LocationEventArgs eventArgs)
        {
            Console.WriteLine("\n******************* Location change ********************");

            Console.WriteLine($"\nCurrernt location = X:{eventArgs.ShipXLocation}, Y:{eventArgs.ShipYLocation}");
            Console.WriteLine($"Moved to location = X:{eventArgs.NewShipXLocation}, Y:{eventArgs.NewShipYLocation}");
        }
        public void GoodSpaceShipDestroyedEventHandler(object? ob, LocationEventArgs eventArgs)
        {
            Console.WriteLine("\n******************* Good spaceship destroyed ********************");

            Console.WriteLine($"\nGood Spaceship was destroyed at Location = X:{eventArgs.ShipXLocation}, Y:{eventArgs.ShipYLocation}");
            Console.WriteLine("\nGame Over!!!");
            return;
        }
        public void BadSpaceShipExplodedEventHandler(object? ob, BadShipExplodedEventArgs eventArgs)
        {
            Console.WriteLine("\n******************* Bad spaceships was destroyed ********************");

            Console.WriteLine($"\n{eventArgs.ShipsExploded} Bad spaceships was destroyed.");
            Console.WriteLine($"Enemy Spaceships left: {eventArgs.CurrentEnemyShipsNumber}");
        }
        public void LevelUpReachedEventHandler(object? ob, LevelEventArgs eventArgs)
        {
            Console.WriteLine("\n******************* LEVEL UP ********************");

            Console.WriteLine("\nLEVEL UP!!!");
            Console.WriteLine($"Level: {eventArgs.CurrentLevel}");
            Console.WriteLine($"enemy Spaceships: {eventArgs.CurrentEnemyShipsNumber}");
        }
    }
}
