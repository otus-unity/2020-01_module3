using System;
using Unity.Entities;
using Unity.AI.Planner.DomainLanguage.TraitBased;
using Generated.AI.Planner.StateRepresentation.Enums;

namespace Generated.AI.Planner.StateRepresentation
{
    [Serializable]
    public struct Agent : ITrait, IEquatable<Agent>
    {
        public const string FieldHealth = "Health";
        public const string FieldAmmo = "Ammo";
        public const string FieldNavigating = "Navigating";
        public const string FieldDistanceToClosestEnemy = "DistanceToClosestEnemy";
        public System.Single Health;
        public System.Single Ammo;
        public System.Boolean Navigating;
        public System.Single DistanceToClosestEnemy;

        public void SetField(string fieldName, object value)
        {
            switch (fieldName)
            {
                case nameof(Health):
                    Health = (System.Single)value;
                    break;
                case nameof(Ammo):
                    Ammo = (System.Single)value;
                    break;
                case nameof(Navigating):
                    Navigating = (System.Boolean)value;
                    break;
                case nameof(DistanceToClosestEnemy):
                    DistanceToClosestEnemy = (System.Single)value;
                    break;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Agent.");
            }
        }

        public object GetField(string fieldName)
        {
            switch (fieldName)
            {
                case nameof(Health):
                    return Health;
                case nameof(Ammo):
                    return Ammo;
                case nameof(Navigating):
                    return Navigating;
                case nameof(DistanceToClosestEnemy):
                    return DistanceToClosestEnemy;
                default:
                    throw new ArgumentException($"Field \"{fieldName}\" does not exist on trait Agent.");
            }
        }

        public bool Equals(Agent other)
        {
            return Health == other.Health && Ammo == other.Ammo && Navigating == other.Navigating && DistanceToClosestEnemy == other.DistanceToClosestEnemy;
        }

        public override string ToString()
        {
            return $"Agent\n  Health: {Health}\n  Ammo: {Ammo}\n  Navigating: {Navigating}\n  DistanceToClosestEnemy: {DistanceToClosestEnemy}";
        }
    }
}
