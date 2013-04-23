using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppReadyGo.API.Models
{
    public class MarketTestersRepository : IMarketTestersRepository
    {

        private Dictionary<String,Tester> mTesters = new Dictionary<String,Tester>();

        /// <summary>
        /// TODO : REMOVE IT !!!!!! FAKE DATA 
        /// </summary>
        internal MarketTestersRepository()
        {
            Add(new Tester { Email = "zubi@gmail.com" , Password = "1234"});
            Add(new Tester { Email = "zubi1@gmail.com", Password = "1234" });
            Add(new Tester { Email = "zub2i@gmail.com", Password = "1234" });
            Add(new Tester { Email = "pasha@appreadygo.com", Password = "1234" });
        }

        public IEnumerable<Tester> GetAll()
        {
            return mTesters.Values;
        }

        public Tester Get(string id)
        {
            return mTesters[id];
        }

        public Tester Add(Tester item)
        {
            if (item == null)
                throw new ArgumentNullException("user is null");
            mTesters.Add(item.Email, item);
            return item;
        }

        public void Remove(string id)
        {
            mTesters.Remove(id);
        }

        public bool Update(Tester item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item is null");
            }
            if (mTesters.Remove(item.Email) == true)
            {
                mTesters.Add(item.Email, item);
                return true;
            }
            else
                return false;
        }
    }
}