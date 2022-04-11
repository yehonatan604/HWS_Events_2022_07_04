namespace HWS_Events_2022_07_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpaceQuestGameManager sq = new(1, 1, 5);
            GameViewer gameViewer = new();
            
            sq.GoodSpaceShipLocationChanged += gameViewer.GoodSpaceShipLocationChangedEventHandler;
            sq.LevelUpReached += gameViewer.LevelUpReachedEventHandler;
            sq.GoodSpaceShipHPChanged += gameViewer.GoodSpaceShipHPChangedEventHandler;
            sq.GoodSpaceShipDestroyed += gameViewer.GoodSpaceShipDestroyedEventHandler;
            sq.BadShipsDestroyed += gameViewer.BadSpaceShipExplodedEventHandler;
            sq.GoodSpaceShipLocationChanged += (sender, eventArgs) => Thread.Sleep(1000); 
            sq.LevelUpReached += (sender, eventArgs) => Thread.Sleep(1000);
            sq.GoodSpaceShipHPChanged += (sender, eventArgs) => Thread.Sleep(1000);
            sq.GoodSpaceShipDestroyed += (sender, eventArgs) => Thread.Sleep(1000);
            sq.BadShipsDestroyed += (sender, eventArgs) => Thread.Sleep(1000);

            Random rnd = new();

            while (true)
            {
                int randomNumber = rnd.Next(1, 5);
                switch (randomNumber)
                {
                    case 1:
                        {
                            sq.MoveSpaceShip(rnd.Next(1, 100), rnd.Next(1, 100));
                            break;
                        }
                    case 2:
                        {
                            sq.GoodSpaceShipGotExtraHp(rnd.Next(1, sq.GoodSpaceShipHitPoints));
                            break;
                        }
                    case 3:
                        {
                            sq.EnemyShipsDestroyed(rnd.Next(1, sq.NumberOfBadShips + 1));
                            break;
                        }
                    case 4:
                        {
                            sq.GoodSpaceShipGotDamage(rnd.Next(1, sq.GoodSpaceShipHitPoints + 5));
                            break;
                        }
                }
                if (sq.GoodSpaceShipHitPoints <= 0)
                {
                    break;
                }
            }
        }
    }
}
