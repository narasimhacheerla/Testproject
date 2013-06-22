using System;
using Huntable.Data;
using Snovaspace.Util.BatchJob;
using Snovaspace.Util.Logging;

namespace Huntable.Business.BatchJobs
{
    public abstract class BatchJobBase : IBatchJob
    {
        private readonly string _name;
        private readonly bool _dbReadOnly;

        protected BatchJobBase(string name, bool dbReadOnly = false)
        {
            _name = name;
            _dbReadOnly = dbReadOnly;

            LoggingManager.Info("Batch Job Constructor " + _name);
        }

        public void Run()
        {
            LoggingManager.Info("Starting to runn batch job " + _name);

            using (var context = huntableEntities.GetEntitiesWithNoLock())
            {
                try
                {
                    Run(context);

                    LoggingManager.Info("Running the job complete.");

                    if (!_dbReadOnly)
                    {
                        LoggingManager.Info("Saving to the database");
                        context.SaveChanges();
                        LoggingManager.Info("Saved to the database");
                    }
                }
                catch (Exception exception)
                {
                    LoggingManager.Error(exception);
                    LoggingManager.Info("Running the job completed unsuccessfully.");
                }
            }
        }

        public abstract void Run(huntableEntities huntableEntities);
    }
}