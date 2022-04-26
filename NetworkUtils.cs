using System;
using System.Net;
using System.Threading;

namespace RadioApp.Network
{
    public static class NetworkUtils
    {
        public static (HttpWebResponse Response, Exception Exception) GetDisposableHttpWebResponse(
            HttpWebRequest httpWebRequest,
            int connectionAttempts = 0,
            int retryAttempts = 4,
            int retryInterval = 2500)
        {
            HttpWebResponse response = null;

            try
            {
                response = httpWebRequest.GetResponse() as HttpWebResponse;

                return (response, null);
            }
            catch (WebException e)
            {
                response?.Dispose();

                if (connectionAttempts < retryAttempts)
                {
                    Thread.Sleep(retryInterval);

                    return GetDisposableHttpWebResponse(httpWebRequest, connectionAttempts + 1, retryAttempts, retryInterval);
                }
                else
                {
                    return (Response: null, Exception: e);
                }
            }
        }

        public static bool IsRemoteAudioSource(string remoteAddress)
        {
            if (WebRequest.Create(remoteAddress) is not HttpWebRequest httpWebRequest)
            {
                return false;
            }

            HttpWebResponse disposableResponse = null;
            bool result = false;

            try
            {
                disposableResponse = GetDisposableHttpWebResponse(httpWebRequest, 2).Response;
                result = disposableResponse.ContentType.StartsWith("audio/");
            }
            catch (InvalidOperationException e)
            {

            }
            finally
            {
                disposableResponse?.Dispose();
            }

            return result;
        }
    }
}
