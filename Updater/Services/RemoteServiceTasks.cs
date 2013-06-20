using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Updater.Models;

namespace Updater.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoteServiceTasks
    {

        /// <summary>
        /// Fetches updater information from the remote update service.
        /// </summary>
        /// <returns></returns>
        public UpdaterInfo FetchUpdaterInfo()
        {
            // Create HTTP POST request to the predefined check-in URL
            HttpWebRequest request = HttpWebRequest.CreateHttp(Configuration.CHECKIN_URI);
            request.Accept = "application/xml";
            request.Method = "GET";

            // Get the response stream and check the status code and content type
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Something went wrong, server did not send 200 OK
                throw new InvalidOperationException("Server did not respond with 200 OK");
            }

            // Deserialize response stream from the XML into CheckinResult
            DataContractSerializer xmlSerializer = new DataContractSerializer(typeof(UpdaterInfo));
            UpdaterInfo result = (UpdaterInfo)xmlSerializer.ReadObject(response.GetResponseStream());

            return result;
        }

        /// <summary>
        /// Fetches available clients from the remote update service.
        /// </summary>
        /// <returns></returns>
        public ClientInfo[] FetchClients()
        {
            // Create HTTP POST request to the predefined check-in URL
            HttpWebRequest request = HttpWebRequest.CreateHttp(Configuration.CLIENTS_URI);
            request.Accept = "application/xml";
            request.Method = "GET";

            // Get the response stream and check the status code and content type
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Something went wrong, server did not send 200 OK
                throw new InvalidOperationException("Server did not respond with 200 OK");
            }

            // Deserialize response stream from the XML into CheckinResult
            DataContractSerializer xmlSerializer = new DataContractSerializer(typeof(ClientListResult));
            ClientListResult result = (ClientListResult)xmlSerializer.ReadObject(response.GetResponseStream());

            return result.Clients;
        }

        /// <summary>
        /// Fetches details of the game client with given System.Int32 identifier from the remote
        /// update service.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public ClientDetails FetchClientDetails(int identifier)
        {
            // Create HTTP POST request to the predefined check-in URL
            HttpWebRequest request = HttpWebRequest.CreateHttp(new Uri(Configuration.CLIENTS_URI, identifier.ToString()));
            request.Accept = "application/xml";
            request.Method = "GET";

            // Get the response stream and check the status code and content type
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Something went wrong, server did not send 200 OK
                throw new InvalidOperationException("Server did not respond with 200 OK");
            }

            // Deserialize response stream from the XML into CheckinResult
            DataContractSerializer xmlSerializer = new DataContractSerializer(typeof(ClientDetails));
            ClientDetails result = (ClientDetails)xmlSerializer.ReadObject(response.GetResponseStream());

            return result;
        }

    }
}
