using System;
using System.Collections.Generic;
using OpenPanel.Models;

namespace OpenPanel.Services
{
    public interface IOpenPanelService
    {
        void GetTopicsAsync(Action<IEnumerable<Topic>> success, Action<Exception> error);
        void VoteAsync(int answerId, Action success, Action<Exception> error);
    }
}

