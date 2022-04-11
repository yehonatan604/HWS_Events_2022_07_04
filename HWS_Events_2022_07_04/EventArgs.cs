namespace HWS_Events_2022_07_04
{
    public class PointsEventArgs : EventArgs
    {
        //properties
        public int OriginalHitPoints { get; set; }
        public int HitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int Damage { get; set; }
        public int ExtraHP { get; set; }
        //ctor
        public PointsEventArgs(int hitPoints, int maxHitPoints, int extraHP = 0, int damage = 0)
        {
            MaxHitPoints = maxHitPoints;
            Damage = damage;
            ExtraHP = extraHP;
            OriginalHitPoints = hitPoints;

            hitPoints = extraHP switch
            {
                0 => hitPoints -= damage,
                _ => hitPoints += extraHP
            };

            HitPoints = hitPoints switch
                {
                    < 0 => 0,
                    > 50 => maxHitPoints,
                    _ => hitPoints
                };
        }
    }
    public class LocationEventArgs : EventArgs
    {
        //properties
        public int ShipXLocation { get; set; }
        public int ShipYLocation { get; set; }
        public int NewShipXLocation { get; set; }
        public int NewShipYLocation { get; set; }
        //ctor
        public LocationEventArgs(int shipXLocation, int shipYLocation, int newShipXLocation = 0, int newShipYLocation = 0)
        {
            ShipXLocation = shipXLocation;
            ShipYLocation = shipYLocation;
            NewShipXLocation = newShipXLocation;
            NewShipYLocation = newShipYLocation;
        }
    }
    public class BadShipExplodedEventArgs : EventArgs
    {
        //properties
        public int ShipsExploded { get; set; }
        public int CurrentEnemyShipsNumber { get; set; }
        //ctor
        public BadShipExplodedEventArgs(int currentEnemyShipsNumber, int shipsExploded)
        {
            ShipsExploded = shipsExploded;
            CurrentEnemyShipsNumber = currentEnemyShipsNumber;
        }
    }
    public class LevelEventArgs : EventArgs
    {
        //properties
        public int CurrentLevel { get; set; }
        public int CurrentEnemyShipsNumber { get; set; }
        //ctor
        public LevelEventArgs(int currentLevel, int currentEnemyShipsNumber)
        {
            CurrentLevel = currentLevel;
            CurrentEnemyShipsNumber = currentEnemyShipsNumber;
        }
    }
}
