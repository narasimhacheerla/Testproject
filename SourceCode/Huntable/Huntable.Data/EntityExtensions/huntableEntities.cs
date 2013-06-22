using Snovaspace.Util.Logging;
namespace Huntable.Data
{
    public partial class huntableEntities
    {
        public static huntableEntities GetEntitiesWithNoLock()
        {
           // LoggingManager.Debug("Entering GetEntitiesWithNoLock  -  huntableEntities.cs");
            var entities = new huntableEntities();
            entities.ExecuteStoreCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");

           // LoggingManager.Debug("Exiting GetEntitiesWithNoLock  -  huntableEntities.cs");

            return entities;
        }

        public void AddObject(UserEmploymentSkill skilltosave)
        {
            throw new System.NotImplementedException();
        }
    }
}
