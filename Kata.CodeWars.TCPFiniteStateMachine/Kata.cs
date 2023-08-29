using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.CodeWars.TCPFiniteStateMachine
{
    public class TCP
    {
        private const int INDEX_INITIAL_STATE = 0;
        private const int INDEX_EVENT = 1;
        private const int INDEX_NEW_STATE = 2;

        private static string datos = "CLOSED: APP_PASSIVE_OPEN -> LISTEN\n" +
                       "CLOSED: APP_ACTIVE_OPEN  -> SYN_SENT\n" +
                       "LISTEN: RCV_SYN          -> SYN_RCVD\n" +
                       "LISTEN: APP_SEND         -> SYN_SENT\n" +
                       "LISTEN: APP_CLOSE        -> CLOSED\n" +
                       "SYN_RCVD: APP_CLOSE      -> FIN_WAIT_1\n" +
                       "SYN_RCVD: RCV_ACK        -> ESTABLISHED\n" +
                       "SYN_SENT: RCV_SYN        -> SYN_RCVD\n" +
                       "SYN_SENT: RCV_SYN_ACK    -> ESTABLISHED\n" +
                       "SYN_SENT: APP_CLOSE      -> CLOSED\n" +
                       "ESTABLISHED: APP_CLOSE   -> FIN_WAIT_1\n" +
                       "ESTABLISHED: RCV_FIN     -> CLOSE_WAIT\n" +
                       "FIN_WAIT_1: RCV_FIN      -> CLOSING\n" +
                       "FIN_WAIT_1: RCV_FIN_ACK  -> TIME_WAIT\n" +
                       "FIN_WAIT_1: RCV_ACK      -> FIN_WAIT_2\n" +
                       "CLOSING: RCV_ACK         -> TIME_WAIT\n" +
                       "FIN_WAIT_2: RCV_FIN      -> TIME_WAIT\n" +
                       "TIME_WAIT: APP_TIMEOUT   -> CLOSED\n" +
                       "CLOSE_WAIT: APP_CLOSE    -> LAST_ACK\n" +
                       "LAST_ACK: RCV_ACK        -> CLOSED";
        public static string TraverseStates(string[] events)
        {

            List<List<string>> actions = GetActions();
            string newState = "CLOSED";
            int i = 0;

            /// Filtering by states
            foreach (var state in events)
            {
                var actionsByDefault = actions
                .Where(x =>
                    x[INDEX_INITIAL_STATE].Equals(newState) &&
                    x[INDEX_EVENT].Equals(events[i]))
                .FirstOrDefault();

                if (actionsByDefault == null)
                    return "ERROR";

                newState = actionsByDefault[INDEX_NEW_STATE];
                i++;
            }

            /// Response
            return newState;
        }

        private static List<List<string>> GetActions()
        {

            var content = datos.Split("\n");
            List<string> splitedLine;
            List<List<string>> actions = new List<List<string>>();
            foreach (var line in content)
            {
                splitedLine = line.Split(new string[] { ":", "->" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                if (splitedLine.Any())
                    actions.Add(splitedLine);
            }
            return actions;
        }
    }
}
