using System;
using System.Collections.Generic;

namespace AppReadyGo.API.Models
{
    public class MarketAppsRepository : IMarketAppsRepository
    {

        private Dictionary<String, Application> mApplications = new Dictionary<string, Application>();

        internal MarketAppsRepository()
        {
            Add(new Application
            {
                Uri = "market://details?id=Security.camera.watchdog",
                Name = "Android WatchDog",
                Description = "WatchDog is a surveillance app that make your phone as a security camera",
                IconUri = "http://qa.mobillify.com/Resources/Feature_TouchMap.jpg",
                Id = "10-10-11",
                ShortDescription = "Cool App"
            });

            Add(new Application
            {
                Uri = "market://details?id=omz.tech.mechde&hl",
                Name = "TechnischenVokabelnTrainer",
                Description = "German Trainer for beginers",
                IconUri = "http://qa.mobillify.com/Resources/Feature_TouchMap.jpg",
                Id = "23-12-11",
                ShortDescription = "Trainer"
            });
            
        }
        public IEnumerable<Application> GetAll()
        {
            return mApplications.Values;
        }

        public Application Get(string id)
        {
            return mApplications[id];
        }

        public Application Add(Application item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item is null");
            }
            mApplications.Add(item.Id, item);
            return item;
        }

        public void Remove(string id)
        {
            mApplications.Remove(id);
        }

        public bool Update(Application item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item is null");
            }
            if (mApplications.Remove(item.Id) == true)
            {
                mApplications.Add(item.Id, item);
                return true;
            }
            else
                return false;
        }
    }
}