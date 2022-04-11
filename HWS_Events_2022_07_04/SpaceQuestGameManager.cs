namespace HWS_Events_2022_07_04
{
    public class SpaceQuestGameManager
    {
        //constant field - maximum HP
        private const int _maxGoodSpaceShipHitPoints = 50;

        //fields
        public int _shipXLocation;
        public int _shipYLocation;
        public int _numberOfBadShips;
        public int _thisLevelNumberOfBadShips;
        public int _currentLevel;
        public int _goodSpaceShipHitPoints;

        //properties
        public int NumberOfBadShips
        {
            get => _numberOfBadShips;
            set => _numberOfBadShips = value switch
            {
                < 0 => 0,
                _ => value
            };
        }
        public int GoodSpaceShipHitPoints
        {
            get => _goodSpaceShipHitPoints;
            set => _goodSpaceShipHitPoints = value switch
            {
                >= _maxGoodSpaceShipHitPoints => _maxGoodSpaceShipHitPoints,
                <= 0 => 0,
                _ => value
            };
        }

        //Event Handlers
        public event EventHandler<PointsEventArgs>? GoodSpaceShipHPChanged;
        public event EventHandler<LocationEventArgs>? GoodSpaceShipLocationChanged;
        public event EventHandler<LocationEventArgs>? GoodSpaceShipDestroyed;
        public event EventHandler<BadShipExplodedEventArgs>? BadShipsDestroyed;
        public event EventHandler<LevelEventArgs>? LevelUpReached;

        //ctor
        public SpaceQuestGameManager(int shipXLocation, int shipYLocation, int numberOfBadShips)
        {
            _currentLevel = 1;
            GoodSpaceShipHitPoints = _maxGoodSpaceShipHitPoints;

            NumberOfBadShips = numberOfBadShips;
            _thisLevelNumberOfBadShips = numberOfBadShips;

            _shipXLocation = shipXLocation;
            _shipYLocation = shipYLocation;

            //initializing EventHandlers

            GoodSpaceShipLocationChanged += (sender, eventArgs) =>
            {
                _shipXLocation = eventArgs.NewShipXLocation;
                _shipYLocation = eventArgs.NewShipYLocation;
            };

            GoodSpaceShipHPChanged += (sender, eventArgs) =>
            {
                GoodSpaceShipHitPoints = eventArgs.ExtraHP switch
                {
                    0 => GoodSpaceShipHitPoints -= eventArgs.Damage,
                    _ => GoodSpaceShipHitPoints += eventArgs.ExtraHP
                };
            };

            BadShipsDestroyed += (sender, eventArgs) => 
            { 
                eventArgs.CurrentEnemyShipsNumber -= eventArgs.ShipsExploded; 
                _numberOfBadShips -= eventArgs.ShipsExploded;
            };
            
            LevelUpReached += (sender, eventArgs) =>
            {
                _thisLevelNumberOfBadShips *= 2;
                NumberOfBadShips = _thisLevelNumberOfBadShips;
                _currentLevel++;
                eventArgs.CurrentEnemyShipsNumber = _thisLevelNumberOfBadShips;
                eventArgs.CurrentLevel = _currentLevel;
            };
        }

        //public methods
        public void MoveSpaceShip(int newX, int newY)
        {
            OnGoodSpaceShipLocationChanged(newX, newY);
        }
        public void GoodSpaceShipGotDamage(int damage)
        {
            OnGoodSpaceShipGotDamage(damage);
        }
        public void GoodSpaceShipGotExtraHp(int extraHP)
        {
            OnSpaceShipGotExtraHp(extraHP);
        }
        public void EnemyShipsDestroyed(int numberOfShipsDestroyed)
        {
            OnBadSpaceShipDestroyed(numberOfShipsDestroyed);
        }

        //private OnEvent methods - invokes eventHandler delegates
        private void OnGoodSpaceShipHPChanged(int extraHP = 0, int damage = 0)
        {
            GoodSpaceShipHPChanged?.Invoke(this, new PointsEventArgs(GoodSpaceShipHitPoints, _maxGoodSpaceShipHitPoints, extraHP, damage));
        }
        private void OnGoodSpaceShipLocationChanged(int newX, int newY)
        {
            GoodSpaceShipLocationChanged?.Invoke(this, new LocationEventArgs(_shipXLocation, _shipYLocation, newX, newY));
        }
        private void OnSpaceShipGotExtraHp(int extraHP)
        {
            OnGoodSpaceShipHPChanged(extraHP: extraHP);
        }
        private void OnGoodSpaceShipGotDamage(int damage)
        {
            OnGoodSpaceShipHPChanged(damage: damage);

            if (GoodSpaceShipHitPoints <= 0)
            {
                OnGoodSpaceShipDestroyed();
            }
        }
        private void OnGoodSpaceShipDestroyed()
        {
            GoodSpaceShipDestroyed?.Invoke(this, new LocationEventArgs(_shipXLocation, _shipYLocation));
        }
        private void OnBadSpaceShipDestroyed(int numberOfShipsDestroyed)
        {
            BadShipsDestroyed?.Invoke(this, new BadShipExplodedEventArgs(_numberOfBadShips, numberOfShipsDestroyed));
            if (_numberOfBadShips <= 0)
            {
                OnLevelUpReached();
            }
        }
        private void OnLevelUpReached()
        {
            LevelUpReached?.Invoke(this, new LevelEventArgs(_currentLevel, _numberOfBadShips));
            GoodSpaceShipHPChanged?.Invoke(this, new PointsEventArgs(GoodSpaceShipHitPoints, GoodSpaceShipHitPoints, _maxGoodSpaceShipHitPoints));
        }
    }
}
