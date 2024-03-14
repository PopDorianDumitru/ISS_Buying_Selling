using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationModule
{
    public class SessionManager
    {
        private Dictionary<Tuple<String, String>, DateTime> session; 
        

        public SessionManager() {
            session = new Dictionary<Tuple<string, string>, DateTime>();
        }

        /*
         * 
         * 
         * We assume that the usernames and the passwords have already been verified and encrypted by the other modules
         * If there already exists a session with those credentials we just update the session time, else we just create the new session
         * We define an "old session" as being a session older than 8 hours
         */

        //get number of existing sessions, does not care wether the sessions are old or not
        public int getNumberOfSessions()
        {
            return session.Count;
        }

        //adds a new session or updates an already existing session
        public void addSession(String username, String pass)
        {
            Tuple<String, String> newKey = Tuple.Create(username, pass);
            session[newKey] = DateTime.Now;
        }

        //removes the session with those credentials, throws error if that session does not exist
        public void removeSession(String username, String pass) 
        {
            Tuple<String, String> oldKey = Tuple.Create(username, pass);
            if (!session.ContainsKey(oldKey))
                throw new KeyNotFoundException("The session you are trying to remove does not exist");
            session.Remove(oldKey);
        }

        //Checks wether a session exists, does not care wether it is old or not
        public bool sessionExists(String username, String pass)
        {
            Tuple<String, String> oldKey = Tuple.Create(username, pass);
            return session.ContainsKey(oldKey);
        }

        //Returns the time that a session was created at, throws error if session does not exist
        public DateTime getSessionTime(String username, String pass)
        {
            Tuple<String, String> key = Tuple.Create(username, pass);
            if (!this.sessionExists(username, pass))
                throw new KeyNotFoundException("That session does not exist");
            return session[key];
        }

        //returns true if the session is recent (started less than 8 hours ago), false otherwise and throws error if session does not exist
        public bool recentSession(String username, String pass)
        {
            Tuple<String,String> key = Tuple.Create(username, pass);
            if (!session.ContainsKey(key))
                throw new KeyNotFoundException("That session does not exist");
            DateTime sessionTime = session[key];
            TimeSpan timeSinceSessionStarted = DateTime.Now - sessionTime;
            if(timeSinceSessionStarted.Days > 0 || timeSinceSessionStarted.Hours > 8)
                return false;
            return true;
        }

        //Renews the session, starting from the present time, throws error if session does not exist
        public void renewSession(String username, String pass)
        {
            Tuple<String, String> key = Tuple.Create(username, pass);
            if (!session.ContainsKey(key))
                throw new KeyNotFoundException("That session does not exist");
            session[key] = DateTime.Now;
        }

        /// <summary>
        /// Deletes any old session still existing (sessions older than 8 hours)
        /// </summary>
        public void deleteOldSessions()
        {
            var keysToDelete = session.Where(sess =>
            {
                TimeSpan timeSinceStartOfSession = DateTime.Now - sess.Value;

                /*if (timeSinceStartOfSession.Milliseconds > 0)
                    return true;*/


                if (timeSinceStartOfSession.Days > 0 || timeSinceStartOfSession.Hours > 8)
                    return true;
                return false;
            });
            foreach (var item in keysToDelete)
                session.Remove(item.Key);
        }

        
       
        

        
    }
}
