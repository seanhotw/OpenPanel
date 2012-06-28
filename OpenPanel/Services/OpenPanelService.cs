using System;
using System.Collections.Generic;
using System.Net;
using Cirrious.MvvmCross.Core;
using Newtonsoft.Json;
using OpenPanel.Models;

namespace OpenPanel.Services
{
    public class OpenPanelService : IOpenPanelService
    {
        private const string ServiceBaseUrl = "http://openpanel.apphb.com/api";

        public OpenPanelService()
        {
        }

        public void GetTopicsAsync(Action<IEnumerable<Topic>> success, Action<Exception> error)
        {
            try
            {
                var webClient = new WebClient();
                webClient.DownloadStringCompleted += (sender, e) => 
                {
                    try
                    {
                        var topics = JsonConvert.DeserializeObject<List<Topic>>(e.Result);
                        success(topics);
                    }
                    catch (Exception iex)
                    {
                        error(iex);
                    }
                };
                webClient.DownloadStringAsync(new Uri(ServiceBaseUrl + "/topics"));
            }
            catch (Exception ex)
            {
                error(ex);
            }
        }
    }
}

