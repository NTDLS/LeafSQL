using LeafSQL.Library.Payloads;
using System;
using System.Net.Http;

namespace LeafSQL.Library.Client
{
    public class LeafSQLClient : IDisposable
    {
        public LoginToken Token { get; set; }
        public HttpClient Client;
        public Management.Document Document { get; set; }
        public Management.Schema Schema { get; set; }
        public Management.Transaction Transaction { get; set; }
        public Management.Query Query { get; set; }
        public Management.Security Security { get; set; }
        public Management.Settings Settings { get; set; }

        #region CTor.

        public LeafSQLClient(string baseAddress)
        {
            Initialize(baseAddress, new TimeSpan(0, 8, 0, 0, 0));
        }

        public LeafSQLClient(string baseAddress, TimeSpan timeout)
        {
            Initialize(baseAddress, timeout);
        }

        public LeafSQLClient(string baseAddress, string username, string password)
        {
            Initialize(baseAddress, new TimeSpan(0, 8, 0, 0, 0));
            Login(username, password);
        }

        public LeafSQLClient(string baseAddress, TimeSpan timeout, string username, string password)
        {
            Initialize(baseAddress, timeout);
            Login(username, password);
        }

        #endregion

        public LoginToken Login(string username, string password)
        {
            return Security.Login(username, password);
        }

        public void Logout()
        {
            Security.Logout();
        }

        private void Initialize(string baseAddress, TimeSpan timeout)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(baseAddress);
            Client.Timeout = timeout;

            Token = new LoginToken();
            Document = new Management.Document(this);
            Schema = new Management.Schema(this);
            Transaction = new Management.Transaction(this);
            Query = new Management.Query(this);
            Security = new Management.Security(this);
            Settings = new Management.Settings(this);
        }

        public void Dispose()
        {
            if (this.Token.IsValid)
            {
                this.Logout();
            }
        }
    }
}