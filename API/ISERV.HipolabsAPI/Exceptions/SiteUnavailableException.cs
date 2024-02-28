namespace ISERV.API.HipolabsAPI.Exceptions
{
    internal class SiteUnavailableException : Exception
    {
        public SiteUnavailableException(string url, string statusCode)
            : base(String.Format($"API {url} unavalible : {statusCode}" ))
        {

        }
    }
}
